using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;
    public float rotateSpeed = 150f;
    public float jumpSpeed = 200f;
    public GameObject bulletPrefab;
    public TimeScale timeScale;

    
    // Use this for initialization
    void Start () {
        timeScale = Utils.getTimeScale();
	}
	
	// Update is called once per frame
	void Update () {
        //Movement
        //TODO: could set up a dictionary of inputs or use delegates and callbacks
        //TODO: Change to using force for movement and rotation?

        /* Joystick Inputs */
        if (Input.GetAxis("Horizontal Stick") != 0)
        {
            transform.localPosition += new Vector3(Input.GetAxis("Horizontal Stick") * Time.deltaTime * moveSpeed, 0, 0);
        }

        if (Input.GetAxis("Vertical Stick") != 0)
        {
            transform.localPosition += new Vector3(0, 0, Input.GetAxis("Vertical Stick") * Time.deltaTime * moveSpeed);
        }

        /* Rotation */
        float rsh = Input.GetAxis("Right Horizontal Stick");
        float rsv = Input.GetAxis("Right Vertical Stick");
        //Don't always reset the angle to 0 if the stick goes dead - wait for the player to move the angle themselves.
        if (rsh != 0 || rsv != 0)
        {
            //Rotate around the y-axis
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(rsh, rsv) * Mathf.Rad2Deg, transform.eulerAngles.z);

            //Fire each of the weapons in the direction the player is pointing
            foreach (Weapon wep in gameObject.GetComponentsInChildren<Weapon>())
            {
                wep.FireBullet();
            }
        }

        /* Key Inputs */
        if (Input.GetButton("Horizontal Key"))
        {
            transform.localPosition += new Vector3(Input.GetAxis("Horizontal Key") * Time.deltaTime * moveSpeed, 0, 0);
        }

        if (Input.GetButton("Vertical Key"))
        {
            transform.localPosition += new Vector3(0, 0, Input.GetAxis("Vertical Key") * Time.deltaTime * moveSpeed);
        }

        if (Input.GetButtonDown("Slow Time"))
        {
            timeScale.slowTime();
        }
        else if (Input.GetButtonDown("Accelerate Time"))
        {
            timeScale.accelerateTime();
        }

    }
}
