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
	
	private float lastStep = 0;
	private Vector3 startPoint = Vector3.zero;
	private Vector3 endPoint = Vector3.zero;

    public Animator EnemiesAnim;

	// Use this for initialization
	void Start () {
		Step = Step / 20F;
		Speed = Speed / 20F;
		startPoint = transform.position;
		endPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		lastStep += Time.deltaTime;

		if (!RandomizeDelay)
				if (lastStep >= Delay) {
					AcquireRoute (Step);
					lastStep = 0;
					MovePhysics();
				}

		if (transform.position == endPoint)
			rigidbody2D.velocity = Vector2.zero;
	}

	void MovePhysics()
	{
		rigidbody2D.velocity = Speed * (endPoint - startPoint).normalized;
	}

	void AcquireRoute (float step)
	{
		Vector3 direction;

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
				
		Horizontal = Direction[Random.Range (0, 3)];
		Vertical = Direction[Random.Range (0, 3)];
		
		return new Vector2(Horizontal, Vertical);
	}

	Vector3 RandomFourWayDirection ()
	{
		int Axis = 0;
		int Horizontal = 0;
		int Vertical = 0;
		int[] Direction = {-1, 1};

		Axis = Random.Range (0, 2);

		if (Axis == 0)
			Horizontal = Direction[Random.Range (0, 2)];
		else
			Vertical = Direction[Random.Range (0, 2)];

		return new Vector2(Horizontal, Vertical);
	}
}
