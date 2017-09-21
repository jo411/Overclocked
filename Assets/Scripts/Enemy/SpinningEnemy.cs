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
    [SerializeField]
    private int maxDirections = 5;

    [SerializeField]
    private LayerMask deadZoneMask;

    public override void Start()
    {
        base.Start();
        changeDirection(0);
    }

    public override void Move()
    {
        moveTime += Time.deltaTime * getTimeScale();
        if (moveTime >= changeDirTime)
        {
            moveTime = 0;
            changeDirection(0);
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

    private void changeDirection(int layer)
    {
        //BANDAID - These enemies are buggy, so we're choosing not to move them for the time being!
        horizontalMove = 0f;
        verticalMove = 0f;
        return;
        //Randomly pick a new direction to go to
        horizontalMove = Random.Range(0.0f, 1.0f);
        verticalMove = 1 - horizontalMove;
        if (Random.Range(0, 1) > 0)
        {
            horizontalMove *= -1;
        }
        if (Random.Range(0, 1) > 0)
        {
            verticalMove *= -1;
        }
        float dist = Mathf.Sqrt(Mathf.Pow(horizontalMove * changeDirTime, 2) + Mathf.Pow(verticalMove * changeDirTime, 2));
        if (Physics.Raycast(transform.position, new Vector3(horizontalMove, 0, verticalMove), dist, deadZoneMask.value))
        {
            //We will be running into a dead zone! Pick another direction
            //TODO: Make this better than sitting around
            Debug.Log("Bad direction.");
            horizontalMove = 0f;
            verticalMove = 0f;
        }
        else
        {
            Debug.Log("Direction: " + horizontalMove + ", " + verticalMove);
        }
    }

}
