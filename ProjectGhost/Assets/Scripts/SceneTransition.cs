using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour 
{
    public string SceneToLoad;
    public PlayerControls playerControls;
    private bool SceneEntered = false;
    public bool HorizontalTransition, VerticalTransition;

    public float FadeTime = 3f;
    public Texture FadeTexture;
    private float AlphaFadeValue = 0f;

	void Start () 
    {
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            StartCoroutine(MoveScene());
    }

    IEnumerator MoveScene()
    {
        SceneEntered = true;
        yield return new WaitForSeconds(FadeTime);
        Application.LoadLevel(SceneToLoad);
    }

    void Update()
    {
        if (SceneEntered)
        {
            if (HorizontalTransition)
                playerControls.h = 1;
            if (VerticalTransition)
                playerControls.v = 1;
            playerControls.enabled = false; 
        }
    }

    void OnGUI()
    {
        if (SceneEntered)
        {
            AlphaFadeValue = Mathf.Clamp01(AlphaFadeValue + (Time.deltaTime / FadeTime));
            GUI.color = new Color(0, 0, 0, AlphaFadeValue);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeTexture);
        }

    }
}
