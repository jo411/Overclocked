using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : MonoBehaviour {
    public float barValue = 1000f;
    public float scaleSpeed = 150f;
    public bool dead = false;
    public UnityEngine.UI.Slider timeBar;
    public GameOverUI gameOver;

    private TimeScale timeScale;

    // Use this for initialization
    void Start()
    {
        timeScale = Utils.getTimeScale();
        if (gameOver == null)
        {
            gameOver = GameObject.Find("GameOverMenu").GetComponentInChildren<GameOverUI>();
        }
    }
    
    void Update()
    { 
        if (!dead)
        {
            // Gain variable bar filling based on scale
            barValue += ((timeScale.getScale() - 1) * scaleSpeed + timeBar.maxValue / 20f) * Time.deltaTime;

            // Forces the time to stay within 0 - 1000
            if (barValue < 0) barValue = 0f;
            if (barValue > 1000) barValue = 1000;
        }
        timeBar.value = barValue;
    }

    public void DecrementTime(float amount)
    {
        barValue -= amount;
        if (barValue <= 0f)
        {
            barValue = 0f;
            // Death stuff here
            dead = true;
            gameOver.endGame();
        }
    }

    public void Reset()
    {
        barValue = timeBar.maxValue;
        dead = false;
    }
}
