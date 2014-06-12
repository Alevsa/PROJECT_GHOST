using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour 
{
    public Texture2D Portrait;
    public string Name;
    public float RadiusOfDetection;

    public Texture2D OptionHighlighter;
    private int HighlightSizeWidth = 72, HighlightSizeHeight = 42;

    public string[] Messages;

    private bool playerNearby, playerInteracting = false, initialInteract = true, secondInteract = false, yesSelected = true;
    private bool Conversation1 = false, Conversation2 = false, questAccepted = false;

    private PlayerControls playersControls;
    private Rigidbody2D playersRigidbody;
    private GUIStyle nameFontStyle, bodyFontStyle;

    private float firstButtonPress, secondButtonPress;

    void Start()
    {
        nameFontStyle = new GUIStyle();
        nameFontStyle.fontSize = 30;
        nameFontStyle.normal.textColor = Color.white;

        bodyFontStyle = new GUIStyle();
        bodyFontStyle.fontSize = 25;
        bodyFontStyle.normal.textColor = Color.white;
        bodyFontStyle.wordWrap = true;

        playersControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        playersRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerNearby = Physics2D.OverlapCircle(this.transform.position, RadiusOfDetection, 1 << LayerMask.NameToLayer("Player"));

        if (playerNearby && Input.GetButtonDown("Fire1") && initialInteract)
        {
            initialInteract = false;
            firstButtonPress = Time.time;
            playerInteracting = true;
        }

        if (Input.GetButtonDown("Fire1") && firstButtonPress != 0)
        {
            if (secondButtonPress == 0)
            {
                if (Messages[1] != null && yesSelected)
                {
                    if (Time.time > firstButtonPress + 1f)
                    {
                        secondButtonPress = Time.time;
                        Conversation1 = true;
                    }
                }

                else if (Messages[2] != null && !yesSelected)
                {
                    if (Time.time > firstButtonPress + 1f)
                    {
                        secondButtonPress = Time.time;
                        Conversation2 = true;
                    }
                }
            }

            else if (Time.time > secondButtonPress + 1f)
            {
                playerInteracting = false;
            }
        }

        if (!playerNearby)
        {
            playerInteracting = false;
        }

        if (playerInteracting)
        {
            playersRigidbody.velocity = new Vector2(0, 0);
            playersControls.PlayersAnim.SetFloat("Horizontal", 0);
            playersControls.PlayersAnim.SetFloat("Vertical", 0);
            playersControls.enabled = false;
        }

        else
        {
            playersControls.enabled = true;
        }
    }

    void OnGUI()
    {
        GUIScaler.BeginGUI();

        if (playerInteracting)
        {
            GUI.Box(new Rect((Screen.width / 1024), Screen.height - 150, Screen.width - 5, 147), "");
            GUI.Label(new Rect((Screen.width / 1024) + 5, Screen.height - 150, 150, 150), Portrait);
            GUI.Label(new Rect((Screen.width / 1024) + 160, Screen.height - 150, 100, 20), Name, nameFontStyle);

            if (Messages[0] != null && !Conversation1 && !Conversation2 && !questAccepted)
            {
                float h = Input.GetAxis("Horizontal");
                if (h < 0)
                    yesSelected = true;
                else if (h > 0)
                    yesSelected = false;

                if (!secondInteract)
                    GUI.Label(new Rect((Screen.width / 1024) + 160, Screen.height - 110, Screen.width - 180, 140), Messages[0], bodyFontStyle);
                else
                    GUI.Label(new Rect((Screen.width / 1024) + 160, Screen.height - 110, Screen.width - 180, 140), Messages[4], bodyFontStyle);

                GUI.Label(new Rect((Screen.width / 1024) + 480, Screen.height - 42, 60, 20), "Yes", bodyFontStyle);
                GUI.Label(new Rect((Screen.width / 1024) + 560, Screen.height - 42, 60, 20), "No", bodyFontStyle);

                if(yesSelected)
                    GUI.DrawTexture(new Rect((Screen.width / 1024) + 467, Screen.height - 48, HighlightSizeWidth, HighlightSizeHeight), OptionHighlighter);
                else
                    GUI.DrawTexture(new Rect((Screen.width / 1024) + 540, Screen.height - 48, HighlightSizeWidth, HighlightSizeHeight), OptionHighlighter);
            }
            
            if (Conversation1)
            {
                GUI.Label(new Rect((Screen.width / 1024) + 160, Screen.height - 110, Screen.width - 180, 140), Messages[1], bodyFontStyle);
                questAccepted = true;
            }
            if (Conversation2)
            {
                GUI.Label(new Rect((Screen.width / 1024) + 160, Screen.height - 110, Screen.width - 180, 140), Messages[2], bodyFontStyle);
                questAccepted = false;
            }
        }

        GUIScaler.EndGUI();
    }

}
