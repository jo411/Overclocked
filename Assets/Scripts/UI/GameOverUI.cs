using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    private int state = 0;
    private bool gameOver = false;
    private Color overlayColor;
    private float flashScale;

    public TimeScale timeScale;
    public GameObject hud;
    public float flashSpeed = .5f;
    public Image overlay;

	// Use this for initialization
	void Start ()
    {
        overlayColor = overlay.color;
        timeScale = Utils.getTimeScale();
        flashScale = 1f / flashSpeed / 60f;
        if (hud == null)
        {
            hud = GameObject.Find("HUD");
        }
    }
	
	
	void FixedUpdate ()
    {
		if (gameOver)
        {
            switch (state)
            {
                // 0: Flash to red
                case 0:
                    overlayColor.g -= flashScale;
                    overlayColor.b -= flashScale;
                    if (overlayColor.g <= 0f)
                    {
                        overlayColor.g = 0f;
                        overlayColor.b = 0f;
                        state = 1;
                    }
                    break;
                // 1: Return to black color
                case 1:
                    overlayColor.g += flashScale;
                    overlayColor.b += flashScale;
                    if (overlayColor.g >= 1f)
                    {
                        overlayColor.g = 1f;
                        overlayColor.b = 1f;
                        state = 2;
                    }
                    break;
                case 2:
                    // Left blank on purpose
                    break;
            }
            overlay.color = overlayColor;
        }
	}

    public void Reset()
    {
        gameOver = false;
        state = 0;
        this.gameObject.SetActive(false);
        hud.SetActive(true);
        timeScale.resetTime();
    }

    public void endGame()
    {
        gameOver = true;
        state = 0;
        this.gameObject.SetActive(true);
        hud.SetActive(false);
        timeScale.resetTime();
    }
}
