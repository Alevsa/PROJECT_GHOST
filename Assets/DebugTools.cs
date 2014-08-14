using UnityEngine;
using System.Collections;

public class DebugTools : MonoBehaviour {

	public GameObject PlayerPrefab;

	// Use this for initialization
	void Start () {
		if (Debug.isDebugBuild)
			if (GameObject.Find ("PlayerMeta") == null)
			{
				//GameObject spawn = GameObject.Find ("SpawnPoint");
				//Vector3 StartCoordinates = new Vector3 (spawn.transform.position.x, spawn.transform.position.y, 0); 
				Object player = Instantiate(PlayerPrefab, GameObject.Find ("SpawnPoint").transform.position, Quaternion.identity);
				GameObject.Find ("Player").transform.position = GameObject.Find ("SpawnPoint").transform.position;
				player.name = "PlayerMeta";
			}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
