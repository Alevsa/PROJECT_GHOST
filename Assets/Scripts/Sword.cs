using UnityEngine;
using System.Collections;

public class Sword : Weapon {
	private GameObject cloneSword;
	public GameObject Sword1;
	private Transform swordSpawn;

	// Use this for initialization
	void Start () {
		swordSpawn = GameObject.Find ("SwordLocation").GetComponent<Transform>();
		AttackAnimation = GameObject.Find("Player").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void Attack() {
		if (swordSpawn == null)
			swordSpawn = GameObject.Find ("SwordLocation").GetComponent<Transform>();

		if (AttackAnimation == null)
			AttackAnimation = GameObject.Find("Player").GetComponent<Animator>();

		cloneSword = (GameObject)Instantiate(Sword1, swordSpawn.position, swordSpawn.rotation);
        cloneSword.transform.parent = swordSpawn;
		AttackAnimation.SetTrigger("SwordAttack");
	}

	public override void EndAttack() {
		Destroy(cloneSword);
	}
}
