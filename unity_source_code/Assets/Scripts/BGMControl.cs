using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMControl : MonoBehaviour
{
    public static BGMControl Instance;

    public AudioSource bgmAudioSource;
    public AudioClip defaultBGM;
    public AudioClip alternativeBGM;

    private void OnEnable()
    {
        ZombieBehavior.OnStateChanged += CheckZombieStates;
    }

    private void OnDisable()
    {
        ZombieBehavior.OnStateChanged -= CheckZombieStates;
    }

    private void CheckZombieStates()
    {
        bool anyChasingTrue = false;
        ZombieBehavior[] zombieStates = FindObjectsOfType<ZombieBehavior>();
        
        foreach (ZombieBehavior zombieState in zombieStates)
        {
            if (zombieState.isChasing)
            {
                anyChasingTrue = true;
            }
        }

        if (anyChasingTrue)
        {
            ChangeBGM(alternativeBGM);
        }
        else
        {
            ChangeBGM(defaultBGM);
        }
    }

    private void ChangeBGM(AudioClip newBGM)
    {
        if (bgmAudioSource.clip != newBGM)
        {
            bgmAudioSource.clip = newBGM;
            bgmAudioSource.Play();
        }
    }
}
