using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    public float moveSpeed;
    public Rigidbody2D rb;

    public bool inBattle = false; //checks if the player is in battle 

    Vector2 movement;
    
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        if (!inBattle)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

          
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
