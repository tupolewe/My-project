using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Animation Variables")]
    [Space]
    int isMovingHash;
    int isAttackingHash;
    public Animator animator;
    [Space]
    
    public float moveSpeed;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public bool inBattle = false; //checks if the player is in battle 
    public PlayerRayCast playerRayCast;
    


    Vector2 movement;

    public void Start()
    {
        animator = GetComponent<Animator>();    
        isMovingHash = Animator.StringToHash("isMoving");
        isAttackingHash = Animator.StringToHash("isAttacking");
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

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            movement.x = Mathf.Abs(horizontalInput) > 0.1f ? horizontalInput : 0;
            movement.y = Mathf.Abs(verticalInput) > 0.34f ? verticalInput : 0;



        }
        

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    #region ANIMATION CONTROLLER
    public bool IsMoving()
    {
        if(movement != Vector2.zero)
        {
            return true;
            
        }
        else { return false; }
        
        
       
    }

 
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
            playerRayCast.rayDirection = Vector2.left;
        }
        else if (movement.x > 0) 
        {
            spriteRenderer.flipX = false;
            playerRayCast.rayDirection = Vector2.right;
        }
    }



    #endregion
}
