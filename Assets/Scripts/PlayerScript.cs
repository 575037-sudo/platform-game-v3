using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playerscript : MonoBehaviour
{

    //declare the variables
    InputAction moveAction;
    InputAction jumpAction;
    InputAction crouchAction;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public float speed = 5;
    bool isGrounded;
    void Start()
    {
        //initialise the variables
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        crouchAction = InputSystem.actions.FindAction("Crouch");



        //required: a 2D Rigidbody component attached to the Game Object
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        isGrounded = false;

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Crouch();
        // read the x-axis and output it to the rigidbody
        Vector2 moveVel = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveVel.x * speed, rb.linearVelocity.y);
        Jump();
        if (rb.linearVelocityX != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {

            anim.SetBool("Walk", false);
        }
        FlipSprite();

    }
    void FlipSprite()
    {
        if (rb.linearVelocityX < -0.1f)
        {
            sr.flipX = false;
        }
        if (rb.linearVelocityX > 0.1f)
        {
            sr.flipX = true;
        }
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
            speed = 1;
        }

    }
    void Jump()
    {
        if (isGrounded == true)
        {
            if (jumpAction.WasPressedThisFrame())
            {

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 3.5f);
            }

        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        //print("isgrounded");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
