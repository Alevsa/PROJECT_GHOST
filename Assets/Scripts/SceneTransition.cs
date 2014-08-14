using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour 
{
    public string SceneToLoad;
	public Vector3 StartCoordinates;
    public PlayerControls playerControls;

    public bool HorizontalRightTransition, HorizontalLeftTransition, VerticalUpTransition, VerticalDownTransition;

    public float FadeTime = 3f;
    public Texture FadeTexture;

    private float AlphaFadeValue = 0f;
	private Rigidbody2D playersRigidbody;
	private MetaData playerData;
	private bool SceneEntered = false;

	void Start () 
    {
		playerData = GameObject.Find ("PlayerMeta").GetComponent<MetaData> ();
        playerControls = GameObject.Find("Player").GetComponent<PlayerControls>();
        playersRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
		{
			playerData.StartCoordinates = new Vector3 (StartCoordinates.x, StartCoordinates.y, StartCoordinates.z);
			StartCoroutine (MoveScene ());
		}
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
            playersRigidbody.velocity = new Vector2(0, 0);
            playerControls.PlayersAnim.SetFloat("Horizontal", 0);
            playerControls.PlayersAnim.SetFloat("Vertical", 0);

            if (HorizontalRightTransition)
            {
                playerControls.h = 1;
                playerControls.v = 0;
            }
            if (HorizontalLeftTransition)
            {
                playerControls.h = -1;
                playerControls.v = 0;
            }
            if (VerticalUpTransition)
            {
                playerControls.h = 0;
                playerControls.v = 1;
            }
            if (VerticalDownTransition)
            {
                playerControls.h = 0;
                playerControls.v = -1;
            }

            playersRigidbody.velocity = new Vector2(playerControls.h * playerControls.moveForce, playerControls.v * playerControls.moveForce);
            playerControls.PlayersAnim.SetFloat("Horizontal", playerControls.h);
            playerControls.PlayersAnim.SetFloat("Vertical", playerControls.v);

            if (Mathf.Abs(playersRigidbody.velocity.x) > playerControls.maxSpeed)
                playersRigidbody.velocity = new Vector2(Mathf.Sign(playersRigidbody.velocity.x) * playerControls.maxSpeed, playersRigidbody.velocity.y);

            if (Mathf.Abs(playersRigidbody.velocity.y) > playerControls.maxSpeed)
                playersRigidbody.velocity = new Vector2(playersRigidbody.velocity.x, Mathf.Sign(playersRigidbody.velocity.y) * playerControls.maxSpeed);

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

	void OnLevelWasLoaded()
	{
		StartCoroutine (DisableWarp ());
	}

	IEnumerator DisableWarp()
	{
		collider2D.enabled = false;
		yield return new WaitForSeconds (FadeTime);
		collider2D.enabled = true;
	}
}
