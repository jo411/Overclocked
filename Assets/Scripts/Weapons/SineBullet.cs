using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : Bullet {

    // Use this for initialization

    
private float shiftx=0;
//private float shifty=0f;

//  [Range(0.0f, 1.0f)]
private float amplitude = .8f;

// [Range(0.0f, 1.0f)]
private float frequency=3f;

private int directionVal = 1;

Vector3 normal;



    public int swapDelayFrames=5;
    public int frameCount;

   



    new void Start()
    {
        base.Start();
        normal = (Quaternion.Euler(0, 90, 0) * direction).normalized;
    }

  
    public override void move()
    {
        frameCount++;


        if (System.Math.Abs(shiftx) >= amplitude&& frameCount>swapDelayFrames)
        {
            frequency *= -1;
            frameCount = 0;
        }

        transform.Translate(direction * moveSpeed * getTimeScale() * Time.deltaTime);

       
        Vector3 shift = new Vector3(shiftx * normal.x * moveSpeed * getTimeScale() * Time.deltaTime, 0, shiftx * normal.z * moveSpeed * getTimeScale() * Time.deltaTime);
      
          
        transform.position += shift;

        shiftx += frequency* getTimeScale() * Time.deltaTime;

    
    }
}
