using UnityEngine;
using System.Collections;

public class MetaData : MonoBehaviour {

	[HideInInspector]
	public Vector3 StartCoordinates;

	[HideInInspector]
	public bool isFromMainMenu;

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
		if (isFromMainMenu)
		{
			playerTransform.position = GameObject.Find ("SpawnPoint").transform.position;
			GetComponent<HUDScript>().enabled = true;
			//GameObject.Find ("Player").GetComponent<SpriteRenderer>().enabled = true;
			isFromMainMenu = false;
			return;
		}

		playerTransform = GameObject.Find ("Player").transform;
		playerControls = gameObject.GetComponentInChildren<PlayerControls>();
		if (StartCoordinates != Vector3.zero)
			playerTransform.position = StartCoordinates;
		else
			playerTransform.position = GameObject.Find ("SpawnPoint").transform.position;

		StartCoordinates = Vector3.zero;
		StartCoroutine (EnableControls ());
	}

	IEnumerator EnableControls()
	{
		yield return new WaitForSeconds (1f);
		playerControls.enabled = true;
	}
}
