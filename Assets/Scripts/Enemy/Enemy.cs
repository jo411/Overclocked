using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    public float moveSpeed = 4f;

    public TimeScale timeScale;

	// Use this for initialization
	public virtual void Start () {
        timeScale = Utils.getTimeScale();
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
        Move();
        Attack();
	}

    /* Leaving this field blank... */
    public virtual void Move()
    {
        
    }



    public virtual void Attack()
    {
        foreach (Weapon wep in gameObject.GetComponentsInChildren<Weapon>())
        {
            wep.FireBullet();
        }
    }
}
