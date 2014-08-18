using UnityEngine;
using System.Collections;

public abstract class Item: MonoBehaviour{

	public int id;
	public string Name;
	public Sprite Icon;
	public string Category;
	public int StackSize;
	public int MaxStackSize;

	// Use this for initialization
}
