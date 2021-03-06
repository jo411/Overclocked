﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float moveSpeed = 0f;

    protected Vector3 direction;

    protected TimeScale timeScale;

    public float personalTimeScale = 1f;

    [SerializeField]
    private float damage = 200f;

    [Range(0.0f, 20f)]
    public float bulletSpeedOverride=0f; //TODO: This is ugly but it seemed better to have bullets have access to setting their own speeds by script

    // Use this for initialization
    protected void Start()
    {
        timeScale = Utils.getTimeScale();        
    }

    // Update is called once per frame
    void Update()
    {
        move();   
    }
    public virtual void move()
    {
        transform.Translate(direction * moveSpeed * getTimeScale() * Time.deltaTime);       
    }
    public void Fire(float fireSpeed, Vector3 fireDirection)
    {
        if(bulletSpeedOverride!=0)
        {
            fireSpeed = bulletSpeedOverride;
        }
        moveSpeed = fireSpeed;
        direction = fireDirection; 
    }

    public void OnCollisionEnter(Collision collision)
    {
        switch(collision.transform.tag)
        {
            case "DeadZone":
                Destroy(this.gameObject);
                break;
            case "Enemy":
                Enemy e = collision.gameObject.GetComponentInChildren<Enemy>();
                e.takeDamage(damage);
                Destroy(this.gameObject);
                break;
            case "Player":
                PlayerController pc = collision.gameObject.GetComponentInChildren<PlayerController>();
                pc.takeDamage(damage);
                Destroy(this.gameObject);
                break;
            case "Bullet":
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
                break;
        }
    }

    protected float getTimeScale()
    {
        return timeScale.getScale() * personalTimeScale;
    }
    public void changeTimeScale(float mult)
    {
        personalTimeScale*= mult;
    }
}