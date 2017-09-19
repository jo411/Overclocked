using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningEnemy : Enemy {

    private float moveTime = 0f;
    [SerializeField]
    private float changeDirTime = 5f;

    private float horizontalMove, verticalMove;

    [SerializeField]
    private float rotateSpeed = 1f;

    public override void Start()
    {
        base.Start();
        changeDirection();
    }

    public override void Move()
    {
        moveTime += Time.deltaTime * getTimeScale();
        if (moveTime >= changeDirTime)
        {
            moveTime = 0;
            changeDirection();
        }
        transform.position += new Vector3(horizontalMove, 0, verticalMove) * getTimeScale() * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotateSpeed * getTimeScale() * Time.deltaTime, 0));
    }

    public override void Attack()
    {
        foreach (Weapon wep in gameObject.GetComponentsInChildren<Weapon>())
        {
            wep.FireBullet();
        }
    }

    private void changeDirection()
    {
        //Randomly pick a new direction to go to
        horizontalMove = Random.Range(0, 1);
        verticalMove = 1 - horizontalMove;
        if (Random.Range(-1, 1) >= 0)
        {
            horizontalMove *= -1;
        }
        if (Random.Range(-1, 1) >= 0)
        {
            verticalMove *= -1;
        }
    }
}
