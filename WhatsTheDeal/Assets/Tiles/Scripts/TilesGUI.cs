using UnityEngine;
using System.Collections;

public class TilesGUI : MonoBehaviour {

    public Texture2D frontTile, backTile;
    public string frontText, backText;
    public float flipDuration;
    public GUISkin tileSkin;

    private float timer, tileRotation, speed;
    private bool flipped;

	// Use this for initialization
	void Start () {

        timer = speed = 0.0f;
        tileRotation = 1.0f;
        flipped = true;
	}

    void Update()
    {
        AnimateDropdown();
    }
	
	// Update is called once per frame
	void OnGUI () {

        GUI.skin = tileSkin;

        DrawTile();
	}

    private void DrawTile()
    {
        if (tileRotation >= 0.0f)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), " "))
            {
                speed = 0.0f;
                flipped = false;
            }
            GUI.DrawTexture(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200 * tileRotation, 400, 400 * tileRotation), frontTile);
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200 * tileRotation, 400, 400 * tileRotation), frontText);
        }
        if (tileRotation < 0.0f)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), " "))
            {
                speed = 0.0f;
                flipped = true;
            }
            GUI.DrawTexture(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200 * tileRotation, 400, 400 * tileRotation), backTile);
            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200 * tileRotation * -1, 400, 400 * tileRotation * -1), backText);
        }
    }

    private void AnimateDropdown()
    {
        timer += Time.deltaTime;

        // If the dropdown is closing, animate up
        if (flipped)
        {
            if (tileRotation != 1.0f)
            {
                tileRotation = Mathf.Lerp(-1.0f, 1.0f, speed);
            }
        }
        // If the dropdown is opening, animate down
        else
        {
            if (tileRotation != -1.0f)
            {
                tileRotation = Mathf.Lerp(1.0f, -1.0f, speed);
            }
        }

        // Animates the dropdown menu based upon the duration in seconds
        if (speed < 1.0f)
        {
            speed += Time.deltaTime / flipDuration;
        }
    }
}
