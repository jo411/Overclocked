using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : Bullet {

    // Use this for initialization

        //TODO: fix issues with first and third quadrents  
    private float shiftx=0f;
    //private float shifty=0f;

    [Range(0.0f, 1.0f)]
    public float amplitude = 1f;

    [Range(0.0f, 1.0f)]
    public float frequency=.084f;
   
    public override void move()
    {
        transform.Translate(direction * moveSpeed * timeScale.getScale() * Time.deltaTime);
               
        Vector3 normal = (Quaternion.Euler(0, -45, 0) * direction).normalized;        
        Vector3 shiftWork = new Vector3(shiftx*normal.x * moveSpeed * timeScale.getScale() * Time.deltaTime, 0, shiftx*normal.z * moveSpeed * timeScale.getScale() * Time.deltaTime);
        transform.position += shiftWork;       

        shiftx += frequency;

        if (System.Math.Abs(shiftx) >= amplitude)
        {
            frequency *= -1;
        }
        
    }
}
