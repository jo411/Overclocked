using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    public float moveSpeed = 4f;

    [SerializeField]
    public float maxHealth = 500f;
    private float health;

    public TimeScale timeScale;

    public float personalTimeScale = 1f;

	// Use this for initialization
	public virtual void Start () {
        timeScale = Utils.getTimeScale();
        health = maxHealth;
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

    protected float getTimeScale()
    {      
        return timeScale.getScale()*personalTimeScale;
    }
    public void changeTimeScale(float mult)
    {
       
        personalTimeScale *= mult;
    }

    public virtual void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            deathSequence();
        }
    }

    //Any on death actions here
    public virtual void deathSequence()
    {
        Destroy(this.gameObject);
    }

}
