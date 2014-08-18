using UnityEngine;
using System.Collections;

public class Weapon : Item {

	public Animator AttackAnimation;
	public int Damage;

	public Weapon()
	{
	}

	virtual public void Attack()
	{

	}

	virtual public void EndAttack()
	{
	}
}
