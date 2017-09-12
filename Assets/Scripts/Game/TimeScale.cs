using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour {

    private float scale = 1f;
    [SerializeField]
    private int baseIndex = 3;
    [SerializeField]
    private float[] scaleValues;

    private int index;
	// Use this for initialization
	void Start () {
        index = baseIndex;
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
        if (index > 0)
        {
            index--;
        }
        scale = scaleValues[index];
    }

    public void accelerateTime()
    {
        if (index < scaleValues.Length)
        {
            index++;
        }
        scale = scaleValues[index];
    }

    public void resetTime()
    {
        index = baseIndex;
        scale = scaleValues[index];
    }
}
