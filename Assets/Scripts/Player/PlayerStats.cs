using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int MaxHealth;
	public int Health;
	public int Money;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Health > MaxHealth)
			Health = MaxHealth;
	}

	void ApplyDamage (int Damage) {
		Health -= Damage;
	}
}
