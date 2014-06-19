using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {
	
	public Texture HealthFullIcon;
	public Texture Health3QuarterIcon;
	public Texture HealthHalfIcon;
	public Texture HealthQuarterIcon;
	public Texture HealthEmptyIcon;
	public Texture MoneyIcon;

	public int IconsPerRow;
	public float HealthPadding;

	public float HealthBarTopLeftX;
	public float HealthBarTopLeftY;
	public float HealthIconSize;

	public float MoneyBarBottomRightX;
	public float MoneyBarBottomRightY;
	public float MoneyBarWidth;
	public float MoneyBarHeight;
	public int MoneyFontSize;

	private Rect HealthBarStartPosition;

	// Use this for initialization
	void Start () {
		MoneyBarWidth *= Screen.width / 100;
		MoneyBarHeight *= Screen.height / 100;
		MoneyFontSize *= Screen.height / 100;

		HealthIconSize *= Screen.width / 100;
		HealthBarStartPosition = new Rect(HealthBarTopLeftX, HealthBarTopLeftY, HealthIconSize, HealthIconSize);
	}
	
	// Update is called once per frame
	void OnGUI () {
		int Health = GetComponent<PlayerStats>().Health;
		int MaxHealth = GetComponent<PlayerStats>().MaxHealth;
		int Money = GetComponent<PlayerStats>().Money;

		Rect HealthBarPosition = HealthBarStartPosition;
		int rowCounter = 1;

		for (int i = 1; i <= MaxHealth / 4; i++)
		{

			GUI.DrawTexture(HealthBarPosition, HealthEmptyIcon);

			if (i <= Health / 4)
				GUI.DrawTexture(HealthBarPosition, HealthFullIcon);
			else
			{
				switch (Health % 4)
				{
				case 1:
					GUI.DrawTexture (HealthBarPosition, HealthQuarterIcon);
					break;

				case 2:
					GUI.DrawTexture (HealthBarPosition, HealthHalfIcon);
					break;

				case 3:
					GUI.DrawTexture (HealthBarPosition, Health3QuarterIcon);
					break;
				}
				Health = 0;
			}

			rowCounter++;

			if (rowCounter > IconsPerRow)
			{
				HealthBarPosition = new Rect (HealthBarStartPosition.xMin, HealthBarPosition.yMin + HealthIconSize + HealthPadding, HealthIconSize, HealthIconSize);
				rowCounter = 1;
			}
			else
				HealthBarPosition = new Rect (HealthBarPosition.xMin + HealthIconSize + HealthPadding, HealthBarPosition.yMin, HealthIconSize, HealthIconSize);
		}

		Rect MoneyBarPosition = new Rect (Screen.width - MoneyBarBottomRightX - MoneyBarWidth, Screen.height - MoneyBarBottomRightY - MoneyBarHeight, MoneyBarWidth, MoneyBarHeight);

		GUI.skin.box.imagePosition = ImagePosition.ImageLeft;
		GUI.skin.box.alignment = TextAnchor.MiddleRight;
		GUI.skin.box.fontSize = MoneyFontSize; 

		GUIContent guiContent = new GUIContent();
		//guiContent.image = MoneyIcon;
		guiContent.text = Money.ToString();

		GUI.Box(MoneyBarPosition, guiContent);


	}
}
