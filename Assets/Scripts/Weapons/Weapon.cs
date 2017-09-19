using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject bulletPrefab;

    private bool canShoot = true;

    public float personalTimeScale = 1f;

    [SerializeField]
    private float bulletSpeed = 12f;
    [SerializeField]
    public float shootDelayTime = 0.2f;
    [SerializeField]
    private bool shootDelayedByTimeScale = true;

    private float timeSinceShot = 0f;
    private TimeScale timeScale;

    public virtual void Start()
    {
        timeScale = Utils.getTimeScale();
    }

    public virtual void Update()
    {
        if (!canShoot)
        {
            if (shootDelayedByTimeScale)
            {
                timeSinceShot += Time.deltaTime * getTimeScale();
            }
            else
            {
                timeSinceShot += Time.deltaTime;
            }
            if (timeSinceShot >= shootDelayTime)
            {
                canShoot = true;
            }
        }
    }

    public virtual void FireBullet()
    {
        if (canShoot)
        {
            Vector3 shootDirection = transform.forward.normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Fire(bulletSpeed, shootDirection);
            canShoot = false;
            timeSinceShot = 0f;
        }
    }
    protected float getTimeScale()//TODO: not sure if weapons have colliders, maybe use send message to children when altering personal timescales
    {
        return timeScale.getScale() * personalTimeScale;
    }
    public void changeTimeScale(float mult)
    {
        personalTimeScale *= mult;
    }
    public virtual void ShootDelay()
    {
        canShoot = true;
    }
}

