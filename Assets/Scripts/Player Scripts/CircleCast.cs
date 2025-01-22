using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CircleCast : MonoBehaviour
{
    [Header("Circle Raycast Settings")]
    public float radius;               // Radius of the circle
    public LayerMask hitLayers;            // Layers to detect
    public Transform raycastOrigin;        // Center of the circle cast

    public List<GameObject> detectedObjects = new List<GameObject>(); // List of detected objects

    void Update()
    {
        PerformCircleRaycast();
    }

    void PerformCircleRaycast()
    {
        // Clear the list of previously detected objects
        detectedObjects.Clear();

        // Perform a circle cast and collect all colliders in the radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(raycastOrigin.position, radius, hitLayers);

        // Process each detected collider
        foreach (Collider2D collider in hitColliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                detectedObjects.Add(collider.gameObject);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("interackja");
                }
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
}
