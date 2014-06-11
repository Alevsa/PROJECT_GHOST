using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour 
{
    private BoxCollider2D playersBoxCollider;

	public float moveForce = 25f;			
	public float maxSpeed = 3f;

    private Animator PlayersAnim;

    void Start()
    {
        playersBoxCollider = GetComponent<BoxCollider2D>();
        PlayersAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        PlayersAnim.SetFloat("Horizontal", h);
        PlayersAnim.SetFloat("Vertical", v);

        if (h * rigidbody2D.velocity.x < maxSpeed || v * rigidbody2D.velocity.y < maxSpeed)
            rigidbody2D.velocity = new Vector2(h * moveForce, v * moveForce);

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

        if (Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sign(rigidbody2D.velocity.y) * maxSpeed);

        if (h == 0)
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);

        if (v == 0)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);

        if (Input.GetButtonDown("Fire2"))
        {
            PlayersAnim.SetTrigger("Attack");
        }
    }
}

