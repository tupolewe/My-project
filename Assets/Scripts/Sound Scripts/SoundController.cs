using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundController : MonoBehaviour
{
    public AudioSource src;
    public AudioClip mainTheme;
    public AudioClip combatTheme;
    public AudioClip clip3;

    private void Start()
    {
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
}
