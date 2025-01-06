using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int isMovingHash; 
    public Animator animator;
    public float moveSpeed;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public bool inBattle = false; //checks if the player is in battle 
    


    Vector2 movement;

    public void Start()
    {
        animator = GetComponent<Animator>();    
        isMovingHash = Animator.StringToHash("isMoving");
    }

    void Update()
    {
        Movement();
        IsMoving();
        AnimationController();
        SpriteFlip();
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


    public bool IsMoving()
    {
        if(movement != Vector2.zero)
        {
            return true;
            
        }
        else { return false; }
        

       
    }

    #region ANIMATION CONTROLLER
    public void AnimationController()
    {
        if (IsMoving())
        {
            animator.SetBool("isMoving", true); 
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void SpriteFlip()
    {
        if(movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0) 
        {
            spriteRenderer.flipX = false;
        }
    }
    #endregion
}
