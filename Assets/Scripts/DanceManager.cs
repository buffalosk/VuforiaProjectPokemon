using System;
using UnityEngine;
using UnityEngine.Events;

public class DanceManager : MonoBehaviour
{
    [SerializeField]
    private String[] danceNames;
    [SerializeField]
    private String[] songNames;
    [SerializeField]
    private Animator character;
    [SerializeField]
    private UnityEvent<Transform> onDanceStart;
    private int currentDanceIndex = 0;

    public void PlayNextDance()
    {
        onDanceStart?.Invoke(character.transform);
        SoundManager.instance.PlayMusic(songNames[currentDanceIndex]);
        character.Play(danceNames[currentDanceIndex]);
        currentDanceIndex++;
        if (currentDanceIndex >= danceNames.Length)
        {
            currentDanceIndex = 0;
        }
    }

    public void StopMusic()
    {
        SoundManager.instance.StopMusic();
    }
}
