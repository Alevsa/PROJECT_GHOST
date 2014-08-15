using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour 
{
    public Animator SlimeAnim;
    bool facingRight;
    private EnemySimple MovementScript;

    //MergeTest

	void Start () 
    {
        MovementScript = this.GetComponent<EnemySimple>();
        SlimeAnim = this.GetComponent<Animator>();
	}
	
	void Update () 
    {
         SlimeAnim.SetBool("Moving", MovementScript.EnemyMoving);
         if (rigidbody2D.velocity.x < 0 && facingRight)
             Flip();

         if (rigidbody2D.velocity.x > 0 && !facingRight)
             Flip();
	}

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
