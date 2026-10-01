using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMove : MonoBehaviour
{
    HelperScript helper;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction crouchAction;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public float speed = 5;
    bool isGrounded;
    public LayerMask jumpableLayerMask;
    bool result;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        crouchAction = InputSystem.actions.FindAction("Crouch");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGrounded = false;
        sr = GetComponent<SpriteRenderer>();

        jumpableLayerMask = LayerMask.GetMask("Jumpable"); //*****
        helper = gameObject.AddComponent<HelperScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Crouch();
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveVel.x * speed, rb.linearVelocity.y);

        print("move xy=" + moveVel.x);

        

        Jump();
        if (rb.linearVelocityX != 0)
        {
            anim.SetBool("Walk", true);

        }
        else
        {
            anim.SetBool("Walk", false);
        }
        isGrounded = RayCollisionCheck(0, 0);
        helper.FlipSprite();
    }
    public bool RayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.1f; // length of raycasts
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, jumpableLayerMask);
 

        Color hitColor = Color.red;

        if (hit.collider != null)
        {
            print("Player has collided with ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }
    
    void Crouch()
    {
        if (crouchAction.WasPressedThisFrame())
        {
            anim.SetBool("Crouch", true);
            speed = 0.33f;
        }
        else if (crouchAction.WasReleasedThisFrame())
        {
            anim.SetBool("Crouch", false);
            speed = 5;
        }    
    }
    void Jump()
    {
        if(isGrounded == true)
        {
            if (jumpAction.WasPressedThisFrame())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 6f);
            }
        }
    }


    
}

