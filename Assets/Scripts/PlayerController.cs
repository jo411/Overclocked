using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;
    public float rotateSpeed = 150f;
    public float jumpSpeed = 200f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

        //Movement
        //TODO: could set up a dictionary of inputs or use delegates and callbacks
        //TODO: Change to using force for movement and rotation?
   
   
        if (Input.GetButton("Horizontal Key")|| Input.GetButton("Horizontal Stick"))
        {
            transform.localPosition += Input.GetAxis("Horizontal Stick")*Input.GetAxis("Horizontal Key")*transform.right * Time.deltaTime * moveSpeed;
        }

        if (Input.GetButton("Vertical Key")|| Input.GetButton("Vertical Stick"))
        {
            transform.localPosition += Input.GetAxis("Vertical Stick") * Input.GetAxis("Vertical Key") * transform.forward * Time.deltaTime * moveSpeed;
        }

        //Rotation    
        if (Input.GetButton("Rotate Key")|| Input.GetButton("Rotate Stick"))
        {
            transform.Rotate(0, Input.GetAxis("Rotate Stick") * Input.GetAxis("Rotate Key")*Time.deltaTime * rotateSpeed, 0);
        }


    }
}
