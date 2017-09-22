using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    public float moveSpeed = 4f;

    [SerializeField]
    public float maxHealth = 500f;
    private float health;

    private Renderer rend;
    private Color color;
    public float colorFlashTime = .2f;

    public TimeScale timeScale;

    public float personalTimeScale = 1f;

	// Use this for initialization
	public virtual void Start () {
        timeScale = Utils.getTimeScale();
        health = maxHealth;
        rend = GetComponentInChildren<Renderer>();
        color = rend.material.color;
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
        else
        {
            StartCoroutine(DamageFeedback());
        }
    }

    //Any on death actions here
    public virtual void deathSequence()
    {
        Destroy(this.gameObject);
    }

    public bool isDead()
    {
        return health <= 0;
    }

    public IEnumerator DamageFeedback()
    {
        rend.material.color = new Color(1f, 0f, 0f);
        yield return new WaitForSeconds(colorFlashTime);
        rend.material.color = color;
    }
}
