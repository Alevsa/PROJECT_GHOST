using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public List<Item> items;

	// Use this for initialization
	void Start () {
		items = new List<Item> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Item i in items)
		{
			Debug.Log (i.Name);
		}
	}

	public void Get (Item item) {
		if (item.MaxStackSize > 1)
		{
			foreach (Item i in items)
			{
				if ((i.id == item.id) && (i.StackSize + item.StackSize <= i.MaxStackSize))
				{
					i.StackSize += item.StackSize;
					break;
				}
			}
		}
		else 
			items.Add (item);
	}

	public void Lose (Item item) {
		foreach (Item i in items) 
		{
			if (i.id == item.id)
			{
				if (i.StackSize < item.StackSize)
				{
					i.StackSize -= item.StackSize;
				}
				else 
					items.Remove(i);

				break;
			}
		}
	}
}
