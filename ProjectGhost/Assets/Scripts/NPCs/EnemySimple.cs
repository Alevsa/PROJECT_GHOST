using UnityEngine;
using System.Collections;

public class EnemySimple : MonoBehaviour {

	public bool Aggressive = false;
	public bool RandomizeDelay = false;
	public bool EightWayMovement = false;
	public float RandomizeByDivider = 2F;


	public float Step;
	public float Speed;
	public float Delay;

	public float AggroRange;

	public int Damage;

	[HideInInspector]
	public bool EnemyMoving = false;
	
	private float lastStep = 0;
	private Vector3 startPoint = Vector3.zero;
	private Vector3 endPoint = Vector3.zero;
	private Vector2 inCollision = Vector2.zero;
	private bool targetAcquired = false;
	private GameObject Player;
	
	void Start () 
    {
		Step = Step / 20F;
		Speed = Speed / 20F;
		startPoint = transform.position;
		endPoint = transform.position;
		Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
    {
		lastStep += Time.deltaTime;

		if (Aggressive) {
			if (Vector3.Distance (Player.transform.position, transform.position) <= AggroRange) {
					targetAcquired = true;
					AcquireRoute (Step);
					MovePhysics ();
					return;
			} else
					targetAcquired = false;
		}

		if (!RandomizeDelay)
		{
			if (lastStep >= Delay) {
					AcquireRoute (Step);
					lastStep = 0;
					MovePhysics ();
			}
		}
		else{
			if (lastStep >= Delay / RandomizeByDivider + Random.Range (0F, Delay/RandomizeByDivider)) {
				AcquireRoute (Step);
				lastStep = 0;
				MovePhysics ();
			}
		}


		if (Vector3.Distance (transform.position, startPoint) >= Vector3.Distance (endPoint, startPoint)) {
						rigidbody2D.velocity = Vector2.zero;
						EnemyMoving = false;
				}
	}

	void MovePhysics()
	{
		rigidbody2D.velocity = Speed * (endPoint - startPoint).normalized;
        StartCoroutine(Moving());
	}

    IEnumerator Moving()
    {
        EnemyMoving = true;
        yield return new WaitForSeconds(Vector3.Distance(endPoint, startPoint)/Speed);
        EnemyMoving = false;
    }

	void AcquireRoute (float step)
	{
		Vector3 direction;

		if (targetAcquired)
		{
			startPoint = transform.position;
			endPoint = Player.transform.position;
			return;
		}

		if (!EightWayMovement)
		{
			direction = RandomFourWayDirection ();
			endPoint = transform.position + direction * Step;
		} 
		else 
		{
			direction = RandomEightWayDirection ();
			endPoint = transform.position + direction * Step;
		}

		startPoint = transform.position;
		endPoint = transform.position + direction * Step;
	}

	Vector3 RandomEightWayDirection()
	{
		int Horizontal = 0;
		int Vertical = 0;
		int[] Direction = {-1, 0, 1};
				
		Horizontal = Direction [Random.Range (0, 3)];
		Vertical = Direction [Random.Range (0, 3)];

		if (inCollision != Vector2.zero) {
			if (inCollision.x - transform.position.x > 0 )
				Horizontal = -1;
			else
				Horizontal = 1;

			if (inCollision.y - transform.position.y > 0 )
				Vertical = -1;
			else
				Vertical = 1;
		}
		
		return new Vector2(Horizontal, Vertical);
	}

	Vector3 RandomFourWayDirection ()
	{
		int Axis = 0;
		int Horizontal = 0;
		int Vertical = 0;
		int[] Direction = {-1, 1};

		Axis = Random.Range (0, 2);

		if (Axis == 0) {
			Horizontal = Direction [Random.Range (0, 2)];

			if (inCollision != Vector2.zero) {
				if (inCollision.x - transform.position.x > 0)
						Horizontal = -1;
				else
						Horizontal = 1;
			}
		} 
		else {
			Vertical = Direction [Random.Range (0, 2)];

			if (inCollision != Vector2.zero) {
				if (inCollision.y - transform.position.y > 0 )
					Vertical = -1;
				else
					Vertical = 1;
			}
		}

		return new Vector2(Horizontal, Vertical);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		rigidbody2D.velocity = Vector2.zero;
		EnemyMoving = false;

		if (collision.collider.tag != "Player")
			inCollision = collision.collider.transform.position;

		if (collision.collider.tag == "Player") {
			collision.collider.gameObject.SendMessage("TakeDamage", Damage);
		}
	}

	void OnCollisionExit2D (Collision2D collision)
	{
		inCollision = Vector2.zero;
	}
}
