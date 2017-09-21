using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;
    public float rotateSpeed = 150f;
    public float jumpSpeed = 200f;
    public GameObject bulletPrefab;
    public TimeScale timeScale;
    public float personalTimeScale = 1f;
    public TimeBar timeBar;

    private Vector3 originPoint;

    // Use this for initialization
    void Start() {
        timeScale = Utils.getTimeScale();
        timeScale.addListener(gameObject);
        originPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeBar.dead)
        {
            readInput();
        }

    }

    private void readInput()
    {
        //Movement

        /* Joystick Inputs */
        if (Input.GetAxis("Horizontal Stick") != 0)
        {
            transform.localPosition += new Vector3(Input.GetAxis("Horizontal Stick") * Time.deltaTime * getTimeScale() * moveSpeed, 0, 0);
        }

        if (Input.GetAxis("Vertical Stick") != 0)
        {
            transform.localPosition += new Vector3(0, 0, Input.GetAxis("Vertical Stick") * Time.deltaTime * getTimeScale() * moveSpeed);
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
            transform.localPosition += new Vector3(Input.GetAxis("Horizontal Key") * Time.deltaTime * getTimeScale() * moveSpeed, 0, 0);
        }

        if (Input.GetButton("Vertical Key"))
        {
            transform.localPosition += new Vector3(0, 0, Input.GetAxis("Vertical Key") * Time.deltaTime * getTimeScale() * moveSpeed);
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

    protected float getTimeScale()
    {
        //return timeScale.getScale() * personalTimeScale; //TODO: do we want the player to be slowed by their own time power
        return personalTimeScale;
    }

    public void changeTimeScale(float mult)
    {
        personalTimeScale *= mult;
    }

    public void takeDamage(float damage)
    {
        timeBar.DecrementTime(damage);
        //Disable hit box
        if (timeBar.dead)
        {
            SetCollision(false);
        }
    }

    public void SetCollision(bool enabled)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = enabled;
        }
    }

    public void ResetPlayer()
    {
        transform.position = originPoint;
        SetCollision(true);
        personalTimeScale = 1f;
    }
}
