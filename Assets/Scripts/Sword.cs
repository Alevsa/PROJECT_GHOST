using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	Weapon sword;
	// Use this for initialization
	void Start () {
		sword = new Weapon ();
		sword.id = 1;
		sword.Name = "Sword";
		sword.Category = "Weapon";
		sword.Damage = 1;
		sword.MaxStackSize = 1;
		sword.StackSize = 1;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Find ("PlayerMeta").GetComponent<Inventory> ().Get (sword);
		Destroy (this.gameObject);
	}
}
