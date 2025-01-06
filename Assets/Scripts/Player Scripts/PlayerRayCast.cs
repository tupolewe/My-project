using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{

    public float rayDistance;
    public LayerMask hitLayers;
    public Vector2 rayDirection = Vector2.right;

    public static Vector2 playerInteractPos; //tracks player position at the moment of interaciton

    public GameObject hitObject;
    
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

            
            // Try to get the Interactable component from the hit object
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            
            // If there's an interactable object and the player presses "E"
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                playerInteractPos = transform.position;
                Debug.Log(playerInteractPos);
                interactable.Interact();
                Debug.Log("Interakcja2");
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
