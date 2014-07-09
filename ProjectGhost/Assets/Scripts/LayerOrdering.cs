using UnityEngine;
using System.Collections;

public class LayerOrdering : MonoBehaviour {

	private SpriteRenderer sprite;
	private float LowestPoint;
	private Transform hui;
	private int sortingorder;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		sprite = GetComponent<SpriteRenderer>();
		LowestPoint = (sprite.transform.position.y - sprite.bounds.size.y / 2);
		sortingorder = Mathf.FloorToInt(10000 / LowestPoint);
		GetComponent<SpriteRenderer>().sortingOrder = sortingorder;
		//Debug.Log(LowestPoint);
	}
}
