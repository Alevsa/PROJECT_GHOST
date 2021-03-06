﻿using UnityEngine;
using System.Collections;

public class IngameMenu : MonoBehaviour {
	
	private float menuWidth = Screen.width / 3;
	private float menuHeight = Screen.height / 2;
	
	private float menuCenterHorizontal = Screen.width / 2;
	private float menuCenterVertical = Screen.height / 2;
	
	private float buttonCount = 5;
	private float buttonSpacing = 10;
	private float buttonPaddingHorizontal = 10;
	private float buttonPaddingVertical = 10;
	private float buttonWidth;
	private float buttonHeight;
	
	// Use this for initialization
	void Start () {
		buttonWidth = menuWidth - buttonPaddingHorizontal * 2;
		buttonHeight = menuHeight / buttonCount - buttonSpacing - buttonPaddingVertical / buttonCount;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (Game.CurrentState == Game.State.InGameMenu)
		{
			GUI.skin.button.fontSize = Mathf.RoundToInt(Screen.width * Camera.main.aspect / 64);
			
			Rect menuPosition = new Rect(menuCenterHorizontal - menuWidth / 2, menuCenterVertical - menuHeight / 2, menuWidth, menuHeight);
			GUI.Box(menuPosition, "");
			
			Rect buttonPosition = new Rect (menuPosition.xMin + buttonPaddingHorizontal, menuPosition.yMin + buttonPaddingVertical, buttonWidth, buttonHeight);
			
			if (GUI.Button (buttonPosition, "Main Menu"))
			{
				Destroy(GameObject.Find ("PlayerMeta"));
				Application.LoadLevel("Main Menu");
			}
			
			buttonPosition.Set (menuPosition.xMin + buttonPaddingHorizontal, buttonPosition.yMin + buttonPosition.height + buttonSpacing, buttonWidth, buttonHeight);
			
			if (GUI.Button (buttonPosition, "Save Game"))
			{
				;
			}
			
			buttonPosition.Set (menuPosition.xMin + buttonPaddingHorizontal, buttonPosition.yMin + buttonPosition.height + buttonSpacing, buttonWidth, buttonHeight);
			
			if (GUI.Button (buttonPosition, "Load Game"))
			{
				;
			}
			
			buttonPosition.Set (menuPosition.xMin + buttonPaddingHorizontal, buttonPosition.yMin + buttonPosition.height + buttonSpacing, buttonWidth, buttonHeight);
			
			if (GUI.Button (buttonPosition, "Options"))
			{
				;
			}

			buttonPosition.Set (menuPosition.xMin + buttonPaddingHorizontal, buttonPosition.yMin + buttonPosition.height + buttonSpacing, buttonWidth, buttonHeight);

			if (GUI.Button (buttonPosition, "Return to Game"))
			{
				Game.Unpause();
			}
		}
	}
}
