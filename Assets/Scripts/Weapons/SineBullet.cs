using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : Bullet {

    // Use this for initialization

        //TODO: fix issues with first and third quadrents 
    private float shiftx=0f;
   //private float shifty=0f;

    [SerializeField]
    public float maxShift = .09f;

    [SerializeField]
    public float frequency=.01f;
   
    public override void move()
    {
        transform.Translate(direction * moveSpeed * timeScale.getScale() * Time.deltaTime);
        Vector3 shiftWork = new Vector3(shiftx, 0, shiftx);
        transform.position += shiftWork;
        
        
        Vector3 shift = new Vector3(shiftx * direction.x, 0, shiftx * direction.z);

       
        Debug.Log(shiftWork);
        

        shiftx += frequency;

        if (System.Math.Abs(shiftx) >= maxShift)
        {
            frequency *= -1;
        }
        
    }
}
