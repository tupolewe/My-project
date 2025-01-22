using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [Header("Circle Raycast Settings")]
    public float radius;               // Radius of the circle
    public LayerMask hitLayers;            // Layers to detect
    public Transform raycastOrigin;        // Center of the circle cast

    public List<GameObject> detectedObjects = new List<GameObject>(); // List of detected objects

    public float rayDistance;
    public Vector2 rayDirection;
    
    public static Vector2 playerInteractPos; //tracks player position at the moment of interaciton
    public static Vector2 npcInteractPos; //tracks enemy position at the moment of interaciton
    public GameObject hitObject;

    public NPC_Interaction npcInteraction;

    public DialogueManager dialogueManager;
    public DialogueScript dialogueScript;

    void Update()
    {
     
       PerformCircleRaycast();
    }



    void PerformCircleRaycast()
    {
     
        detectedObjects.Clear();
      
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(raycastOrigin.position, radius, hitLayers);

        foreach (Collider2D collider in hitColliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                detectedObjects.Add(collider.gameObject);
                hitObject = collider.gameObject;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    dialogueManager.currentDialogueScript = dialogueScript;
                    playerInteractPos = transform.position;
                    npcInteractPos = collider.transform.position;
                    interactable.Interact();
                }
            }
            else
            {
                interactable = null;
            }

        }



        // Visualize the circle in the Scene view for debugging
        DebugDrawCircle(raycastOrigin.position, radius, Color.red);
    }

    void DebugDrawCircle(Vector2 center, float radius, Color color)
    {
        int segments = 36; // Number of segments to approximate the circle
        float angleIncrement = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * angleIncrement * Mathf.Deg2Rad;
            float angle2 = (i + 1) * angleIncrement * Mathf.Deg2Rad;

            Vector2 point1 = center + new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
            Vector2 point2 = center + new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;

            Debug.DrawLine(point1, point2, color);
        }
    }




    //public void RayCastInteraction () 
    //{
    //    // Cast a ray from the player's position in the specified direction
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance, hitLayers);

    //    // Draw the ray in the Scene view for debugging
    //    Debug.DrawRay(transform.position, rayDirection * rayDistance, Color.red);

    //     if (hit.collider != null)
    //    {



    //        Interactable interactable = hit.collider.GetComponent<Interactable>();
    //        //npcInteraction.dialogueScript = hit.collider.GetComponent <DialogueScript>();

    //        if (interactable != null && Input.GetKeyDown(KeyCode.E))
    //        {

    //            dialogueManager.currentDialogueScript = dialogueScript;
    //            interactable.Interact();



    //            //playerInteractPos = transform.position;
    //            //npcInteractPos = hit.collider.transform.position;
    //            //interactable.Interact();
    //            //Debug.Log("Interakcja");


    //        }
    //        else if (interactable != null)
    //        {
    //            hitObject = hit.collider.gameObject;
    //        }

    //    }
    //    else
    //    {
    //        hitObject = null;    
    //    }
    //}
}
