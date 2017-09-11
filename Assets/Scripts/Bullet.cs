using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float moveSpeed = 0f;

    private Vector3 direction;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    public void Fire(float fireSpeed, Vector3 fireDirection)
    {
        moveSpeed = fireSpeed;
        direction = fireDirection;
    }
}