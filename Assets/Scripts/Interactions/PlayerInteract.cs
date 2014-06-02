using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour 
{
    public Texture2D Portrait;
    public string Name;
    public float RadiusOfDetection;

    public string[] Messages;

    private bool playerNearby, playerInteracting = false;

    void Update()
    {
        playerNearby = Physics2D.OverlapCircle(this.transform.position, RadiusOfDetection, 1 << LayerMask.NameToLayer("Player"));

        if (playerNearby && Input.GetButtonDown("Fire1"))
        {
            playerInteracting = true;
        }

        if (!playerNearby)
        {
            playerInteracting = false;
        }
    }

    void OnGUI()
    {
        GUIScaler.BeginGUI();

        if (playerInteracting)
        {
            //GUI.Label(new Rect((Screen.width / 1024), Screen.height, Screen.width, 160), );
            GUI.Label(new Rect((Screen.width/1024) + 5, Screen.height - 150, 150, 150), Portrait);
            GUI.Label(new Rect((Screen.width / 1024) + 160, Screen.height - 150, 100, 20), Name);
        }

        GUIScaler.EndGUI();
    }

}
