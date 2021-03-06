﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpField : MonoBehaviour
{
    private float[] factors = new float[] {.125f,.25f,.5f,1f,2f };
    public float warpFactor = 1f;
    public float warpRadius = 1f;//this should be a power of 2 for float reasons
    HashSet<GameObject> insideField;//which objects are currently inside the collider in case it gets destroyed.

    public GameObject indicator;
    private SphereCollider field;
    // Use this for initialization
    void Start()
    {
        indicator = Instantiate(indicator,gameObject.transform);
        indicator.transform.position = gameObject.transform.position;

        insideField = new HashSet<GameObject>();

        field = gameObject.AddComponent<SphereCollider>();
        field.isTrigger = true;
        field.radius = warpRadius;

        indicator.transform.localScale = new Vector3(warpRadius*2, .001f, warpRadius*2);
                    
        
    }

    void OnDestroy()
    {
        //cleanup remaining objects inside field
        foreach(GameObject current in insideField)
        {
            if(current!=null)//bullets still exist in the field
            {
                sendScaleMessage(current, 1 / warpFactor);
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Modifying"); 
        insideField.Add(other.gameObject);
        sendScaleMessage(other.gameObject, warpFactor);
    }
    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("Reverting");  
        insideField.Remove(other.gameObject);
        sendScaleMessage(other.gameObject, 1/warpFactor);
    }
   
    private void sendScaleMessage(GameObject other,float scaleMult)
    {
        other.SendMessage("changeTimeScale", scaleMult, SendMessageOptions.DontRequireReceiver);
    }
}
