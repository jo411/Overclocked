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

    public void FireBullet(float rightStickHorizontal, float rightStickVertical)
    {
        if (canShoot)
        {
            Vector3 shootDirection = new Vector3(rightStickHorizontal, 0, rightStickVertical).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Fire(bulletSpeed, shootDirection);
            canShoot = false;
            Invoke("ShootDelay", shootDelayTime);
        }
    }

    public void ShootDelay()
    {
        canShoot = true;
    }
}

