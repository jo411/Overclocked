using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour {

    private float scale = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getScale()
    {
        return scale;
    }

    public void slowTime()
    {
        if (scale > 0.125)
        {
            scale /= 2;
        }
        Debug.Log(scale);
    }

    public void accelerateTime()
    {
        if (scale < 1)
        {
            scale *= 2;
        }
        else if (scale >= 1 && scale < 2)
        {
            scale += .25f;
        }
        Debug.Log(scale);
    }

    public void resetTime()
    {
        scale = 1f;
        Debug.Log(scale);
    }
}
