using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour 
{
	public float moveForce = 25f;			
	public float maxSpeed = 3f;
    public float h, v;

    public Animator PlayersAnim;

    void Start()
    {
		Game.Unpause();
        PlayersAnim = GetComponent<Animator>();
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
		{
			if (Game.CurrentState == Game.State.Running)
			{
				Game.Pause();
			}
			else if (Game.CurrentState == Game.State.Paused)
			{
				Game.Unpause();
			}
		}
	}

    void FixedUpdate()
	{
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

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

