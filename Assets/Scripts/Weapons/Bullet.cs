using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float moveSpeed = 0f;

    protected Vector3 direction;

    protected TimeScale timeScale;

    [SerializeField]
    private float damage = 200f;

    [Range(0.0f, 20f)]
    public float bulletSpeedOverride=0f; //TODO: This is bad but it seemed better to have bullets have access to setting their own speeds by script

    // Use this for initialization
    void Start()
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
        transform.Translate(direction * moveSpeed * timeScale.getScale() * Time.deltaTime);  
    }
    public void Fire(float fireSpeed, Vector3 fireDirection)
    {
        if(bulletSpeedOverride!=0)
        {
            fireSpeed = bulletSpeedOverride;
        }
        moveSpeed = fireSpeed;
        direction = fireDirection;
        Debug.Log(fireDirection);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "DeadZone")
        {
            Destroy(this.gameObject);
        }

        else if (collision.transform.tag == "Enemy")
        {
            Destroy(collision.transform.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.transform.tag == "Player")
        {
            PlayerController pc = collision.gameObject.GetComponentInChildren<PlayerController>();
            pc.takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}