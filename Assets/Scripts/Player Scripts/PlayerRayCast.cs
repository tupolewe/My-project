using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{

    public float rayDistance;
    public LayerMask hitLayers;
    public Vector2 rayDirection;
    
    public static Vector2 playerInteractPos; //tracks player position at the moment of interaciton
    public static Vector2 npcInteractPos; //tracks enemy position at the moment of interaciton
    public GameObject hitObject;

    public NPC_Interaction npcInteraction;

    public DialogueManager dialogueManager;
    public DialogueScript dialogueScript;

    void Update()
    {
        RayCastInteraction();
    }

    public void RayCastInteraction () 
    {
        // Cast a ray from the player's position in the specified direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance, hitLayers);

        // Draw the ray in the Scene view for debugging
        Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.red);

         if (hit.collider != null)
        {

            
            
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            //npcInteraction.dialogueScript = hit.collider.GetComponent <DialogueScript>();

            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                
                dialogueManager.currentDialogueScript = dialogueScript;
                interactable.Interact();
               


                //playerInteractPos = transform.position;
                //npcInteractPos = hit.collider.transform.position;
                //interactable.Interact();
                //Debug.Log("Interakcja");
                

            }
            else if (interactable != null)
            {
                hitObject = hit.collider.gameObject;
            }

        }
        else
        {
            hitObject = null;    
        }
    }

   
        
}
