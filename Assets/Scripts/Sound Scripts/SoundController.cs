using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource src;
    public AudioClip mainTheme;
    public AudioClip combatTheme;
    public AudioClip bolecHideout;
    public AudioClip lysyTheme;

    public Dictionary<string, Action> soundMethods;

    public void Start()
    {
        
        soundMethods = new Dictionary<string, Action>
        {
            { "MainTheme", MainTheme },
            { "CombatTheme", CombatTheme },
            { "BolecHideOut", BolecHideOut },
            { "AttackSound", AttackSound },
            { "LysyTheme", LysyTheme },
        };

        MainTheme();
    }

    public void CombatTheme()
    {
        src.clip = combatTheme;
        src.Play();
    }

    public void MainTheme()
    {
        src.clip = mainTheme;
        src.Play();
    }

    public void AttackSound()
    {
        src.clip = combatTheme;
        src.Play();
    }

    public void BolecHideOut()
    {
        src.clip = bolecHideout;
        src.Play();
    }
    public void LysyTheme()
    {
        src.clip = lysyTheme;   
        src.Play();
    }
    
    public Dictionary<string, Action> GetSoundMethods()
    {
        return soundMethods;
    }
}
