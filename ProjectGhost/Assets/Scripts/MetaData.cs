using UnityEngine;
using System.Collections;

public class MetaData : MonoBehaviour {

	[HideInInspector]
	public Vector3 StartCoordinates;

	[HideInInspector]
	public bool NewGame = true;

	private PlayerControls playerControls;
	private Transform playerTransform;

	// Use this for initialization
	void Start () {
		playerTransform = GameObject.Find ("Player").transform;
		playerControls = gameObject.GetComponentInChildren<PlayerControls>();
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded (int level) {
		if (NewGame)
		{
			playerTransform.position = GameObject.Find ("SpawnPoint").transform.position;
			//GetComponent<HUDScript>().enabled = true;
			//GameObject.Find ("Player").GetComponent<SpriteRenderer>().enabled = true;
			NewGame = false;
			return;
		}

		playerTransform = GameObject.Find ("Player").transform;
		playerControls = gameObject.GetComponentInChildren<PlayerControls>();
		playerTransform.position = StartCoordinates;
		StartCoroutine (EnableControls ());
	}

	IEnumerator EnableControls()
	{
		yield return new WaitForSeconds (1f);
		playerControls.enabled = true;
	}
}
