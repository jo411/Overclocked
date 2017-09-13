using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : MonoBehaviour {
    public float barValue = 1000f;
    public UnityEngine.UI.Slider timeBar;

    private TimeScale timeScale;

    // Use this for initialization
    void Start()
    {
        timeScale = Utils.getTimeScale();
    }
    
    void Update()
    { 
        // Gain variable bar filling based on scale
        barValue += (timeScale.getScale() - 1) * 50 * Time.deltaTime;

        // Forces the time to stay within 0 - 1000
        if (barValue < 0) barValue = 0;
        if (barValue > 1000) barValue = 1000;

        timeBar.value = barValue;
    }
}
