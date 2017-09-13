using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveSpeed = 0f;

    private Vector3 direction;

    private TimeScale timeScale;

    [SerializeField]
    private float damage = 200f;

    // Use this for initialization
    void Start()
    {
        timeScale = Utils.getTimeScale();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * timeScale.getScale() * Time.deltaTime);
    }

    public void Fire(float fireSpeed, Vector3 fireDirection)
    {
        moveSpeed = fireSpeed;
        direction = fireDirection;
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