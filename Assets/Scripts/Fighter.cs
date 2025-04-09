using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onCharacterStart;
    [SerializeField]
    private UnityEvent onAttack;
    private Fighter enemyFighter;
    [SerializeField]
    private float minDamage = 5f;
    [SerializeField]
    private float maxDamage = 20f;
    [SerializeField]
    private float attackDuration = 1f;
    public float AttackDuration => attackDuration;

    public void OnCharacterStart()
    {
        onCharacterStart?.Invoke();
    } 

    public void SetEnemyFighter(Fighter enemy)
    {
        enemyFighter = enemy;
    }

    public void Attack()
    {
        onAttack?.Invoke();
    }

    public float GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
}
