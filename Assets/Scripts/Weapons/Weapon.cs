using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject bulletPrefab;

    private bool canShoot = true;

    [SerializeField]
    private float bulletSpeed = 12f;
    [SerializeField]
    public float shootDelayTime = 0.2f;

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
            timeSinceShot += Time.deltaTime * timeScale.getScale();
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

    public virtual void ShootDelay()
    {
        canShoot = true;
    }
}

