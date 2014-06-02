using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour 
{
	public bool facingRight = true;

	public float moveForce = 25f;			
	public float maxSpeed = 3f;	

    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h * rigidbody2D.velocity.x < maxSpeed && v * rigidbody2D.velocity.y < maxSpeed)
            rigidbody2D.velocity = new Vector2(h * moveForce, v * moveForce);

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

        if (Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sign(rigidbody2D.velocity.y) * maxSpeed);

        if (h == 0)
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);

        if (v == 0)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);

        if (h > 0 && !facingRight)
            Flip();

        else if (h < 0 && facingRight)
            Flip();

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

