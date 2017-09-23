using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRegen : MonoBehaviour {

    public float regenAmount = 50f;
    public float regenSpeed = 1f;
    public float duration = 20f;
    public TimeBar timeBar;

    private float timeElapsed = 0f;
    private float effectTimer = 0f;

	// Use this for initialization
	void Start () {
        timeBar = GameObject.Find("Canvas").GetComponent<TimeBar>();
	}
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        effectTimer += Time.deltaTime;

        if(timeElapsed > regenSpeed)
        {
            timeBar.IncrementTime(regenAmount);
            timeElapsed -= regenSpeed;
        }

        if(effectTimer > duration)
        {
            Destroy(this);
        }
	}

    public void SetRegenAmount(float amout)
    {
        regenAmount = amout;
    }

    public void SetRegenSpeed(float amout)
    {
        regenSpeed = amout;
    }
}
