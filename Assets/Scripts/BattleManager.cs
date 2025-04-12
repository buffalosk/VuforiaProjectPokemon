using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{

    [SerializeField]
    private List<Fighter> fighters = new List<Fighter>();
    [SerializeField]
    private int requiredFighters = 2;
    [SerializeField]
    private float secondsBetweenAttacks = 1f;
    [SerializeField]
    private float secondsToStartBattle = 1f;
    [SerializeField]
    public UnityEvent onBattleStart;
    [SerializeField]
    private UnityEvent onBattleStop;
    [SerializeField]
    private UnityEvent onBattleEnd;
    [SerializeField]
    private UnityEvent<string> onFighterWins;
    private int currentFighterIndex = 0;
    private bool isBattleActive = false;
    private Coroutine attackCoroutine;
    public void AddFighter(Fighter fighter)
    {
        fighters.Add(fighter);
        CheckFighters();
    }
    public void RemoveFighter(Fighter fighter)
    {
        fighters.Remove(fighter);
        CheckFighters();
    }

    private void CheckFighters()
    {
        if (fighters.Count < requiredFighters)
        {
            StopBattle();
        }
        else
        {
            Invoke("StartBattle", secondsToStartBattle);
        }
    }

    private void StartBattle()
    {
        if (isBattleActive || fighters.Count < requiredFighters)
        {
            return;
        }
        isBattleActive = true;
        onBattleStart?.Invoke();
        attackCoroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (!isBattleActive)
        {
            yield break;
        }
        currentFighterIndex = Random.Range(0, fighters.Count);
        Fighter attacker = fighters[currentFighterIndex];
        Fighter defender;
        do
        {
            currentFighterIndex = Random.Range(0, fighters.Count);
            defender = fighters[currentFighterIndex];
        } while (attacker == defender);

        attacker.transform.LookAt(defender.transform.position);
        defender.transform.LookAt(attacker.transform.position);

        attacker.Attack();
        yield return new WaitForSeconds(attacker.AttackDuration);
        float damage = attacker.GetDamage();
        defender.GetComponent<Health>().TakeDamage(damage);

        yield return new WaitForSeconds(secondsBetweenAttacks);
        if (defender.GetComponent<Health>().CurrentHealth > 0)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
        else
        {
            BattleFinish(attacker.FighterName);
        }
    }

    private void BattleFinish(string winnerName)
    {
        StopBattle();
        onBattleEnd?.Invoke();
        onFighterWins?.Invoke(winnerName);
    }

    private void StopBattle()
    {
        isBattleActive = false;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        onBattleStop?.Invoke();
    }
}
