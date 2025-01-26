using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationChange : MonoBehaviour, Interactable
{
    public Transform newPos;               
    public SoundController soundController; 
    public GameObject player;              

    public PlayerRayCast playerRayCast;    

    public string musicMethodName;         

    public void Interact()
    {
        
        player.transform.position = newPos.position;

        
        if (soundController != null && !string.IsNullOrEmpty(musicMethodName))
        {
            var soundMethods = soundController.GetSoundMethods();
            if (soundMethods.TryGetValue(musicMethodName, out var method))
            {
                method.Invoke();
            }
            else
            {
                Debug.LogError($"Music method '{musicMethodName}' not found in SoundController.");
            }
        }
    }
}
