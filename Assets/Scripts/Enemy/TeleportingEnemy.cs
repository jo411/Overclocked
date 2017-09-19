using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingEnemy : FollowEnemy {

  
    public float tpRadius = 1f;
    public float tpDelay = 10f;  
    private float tpTimer = 0f;

  

    public override void Start()
    {
        base.Start();        
    }


    public override void Move()
    {
        tpTimer += Time.deltaTime * getTimeScale();
        if (tpTimer >= tpDelay)
        {
            tpTimer = 0;
            teleport();
        }

        Vector3 distance = (target.transform.position - transform.position).normalized;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distance), rotateSpeed * getTimeScale() * Time.deltaTime);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

  
    public void teleport()
    {
        if(Vector3.Distance(transform.position,target.transform.position)>tpRadius)
        {
            Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, tpRadius);
            transform.position = new Vector3(movement.x, transform.position.y, movement.z);

        }
    }


}
