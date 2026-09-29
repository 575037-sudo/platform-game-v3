using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public LayerMask jumpableLayerMask;
    bool result;
    bool isGroundedLeft, isGroundedMiddle, isGroundedRight;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float dirX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpableLayerMask = LayerMask.GetMask("Jumpable");
        sr = GetComponent<SpriteRenderer>();
        dirX = 2;
    }

    // Update is called once per frame
    void Update()
    {
        print("player x position is " + target.transform.position.x);

        isGroundedLeft = RayCollisionCheck(-0.2f, 0);
        isGroundedMiddle = RayCollisionCheck(0, 0);
        isGroundedRight = RayCollisionCheck(0.2f, 0);
        
        if (/*isGroundedRight == false && (dirX>0) ||*/ target.transform.position.x == transform.position.x -3)
        {
            dirX = -2;

         
        }
        if (/*isGroundedLeft == false && (dirX < 0)|| */target.transform.position.x == transform.position.x +3)
        {
            dirX = +2;


        }
        
        rb.linearVelocityX = dirX;

        Flipsprite();
    }
    void Flipsprite()
    {
        if (target.transform.position.x < transform.position.x)
        {
            sr.flipX = false;
        }
        else if (target.transform.position.x > transform.position.x)
        {
            sr.flipX = true;
        }
        /* if (dirX < 0.1f)
        {
            sr.flipX = false;

        }
        else if (dirX > -0.1f)
        {
            sr.flipX = true;
        }
        */
    }
    public bool RayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.5f; // length of raycasts
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
}
