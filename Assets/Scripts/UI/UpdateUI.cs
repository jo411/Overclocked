using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateUI : MonoBehaviour {

    [SerializeField]
    private Text timeText;

    private TimeScale timeScale;

	// Use this for initialization
	void Start ()
    {
        timeScale = Utils.getTimeScale();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeText.text = "x" + timeScale.getScale().ToString();
	}
}
