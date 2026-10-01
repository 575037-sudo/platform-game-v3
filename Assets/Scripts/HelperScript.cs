using UnityEngine;

public class HelperScript : MonoBehaviour
{

    public void FlipSprite()
    {
        Rigidbody2D rb;
        
        SpriteRenderer sr;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (rb.linearVelocityX < -0.1f)
        {
            sr.flipX = false;
        }
        if (rb.linearVelocityX > 0.1f)
        {
            sr.flipX = true;
        }

    }

}