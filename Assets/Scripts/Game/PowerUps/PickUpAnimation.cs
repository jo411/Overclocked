using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAnimation : MonoBehaviour {
    public float height = 1f;
    public float moveSpeed = 3f;
    public float rotateSpeed = 50f;
    public TimeScale timeScale;

    Vector3 startPosition;
    private float shiftY;
    private int direction = 1;

    // Use this for initialization
    void Start () {
        timeScale = Utils.getTimeScale();
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        rotate(Time.deltaTime);
        bounce(Time.deltaTime);
	}

    public void rotate(float deltaTime)
    {
        transform.Rotate(Vector3.up, timeScale.getScale() * deltaTime * rotateSpeed);
    }

    public void bounce(float deltaTime)
    {
        Vector3 shift = Vector3.up * (Time.deltaTime * timeScale.getScale() * moveSpeed);
        shift.y *= direction;
        shiftY += shift.y;
        if(shiftY>=height || shiftY <=0)
        {
            direction *= -1;
        }
        transform.position += shift;
    }
}
