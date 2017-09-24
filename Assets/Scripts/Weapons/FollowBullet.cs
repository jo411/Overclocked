using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : Bullet
{ 

    public float lifetime = 4f;
    // Use this for initialization
    private GameObject target;

    public float rotateSpeed=1f;

	new void Start () {
        Destroy(gameObject, lifetime);//kill after time
        base.Start();
        target = GameObject.Find("Player");
        GetComponent<MeshRenderer>().material.color = Color.black;
    }
	

    public override void move()
    {
        Vector3 distance = (target.transform.position - transform.position);

       
            Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * getTimeScale() * Time.deltaTime);
            transform.position = new Vector3(movement.x, transform.position.y, movement.z);
        

        distance = distance.normalized;

        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distance), rotateSpeed * getTimeScale() * Time.deltaTime);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
