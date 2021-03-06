﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : Enemy {

    protected GameObject target;

    [SerializeField]
    public float rotateSpeed = 1f;
 

    public override void Start()
    {
        base.Start();
        target = GameObject.Find("Player");
    }

    public override void Move()
    {
        Vector3 distance = (target.transform.position - transform.position);

        if (distance.magnitude > 2f)
        {
            Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * getTimeScale() * Time.deltaTime);
            transform.position = new Vector3(movement.x, transform.position.y, movement.z);
        }

        distance = distance.normalized;

        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(distance), rotateSpeed * getTimeScale() * Time.deltaTime);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,0);
    }

    public override void Attack()
    {
        foreach (Weapon wep in gameObject.GetComponentsInChildren<Weapon>())
        {
            wep.FireBullet();
        }
    }
}
