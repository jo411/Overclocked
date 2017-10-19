using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheAttack : MonoBehaviour
{
    private bool isAttacking = false;
    private Vector3 origPos;
    private Quaternion origRot;
    private float step;
    private float stepTime;
    private MeshRenderer[] rends;


    public GameObject origin;
    //public float angleFrom = 60f, angleTo = -60f;
    public float rotateAmount = 180f;
    public float attackTime = .2f;
    public float damage = 350f;

    // Use this for initialization
    void Start()
    {
        rends = GetComponentsInChildren<MeshRenderer>();
        SetRenderers(rends, false);
        Utils.setCollision(this.gameObject, false);

        step = rotateAmount / 60f / attackTime;
        stepTime = 1f / (rotateAmount / attackTime);
        origPos = transform.localPosition;
        origRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRenderers(MeshRenderer[] renderers, bool enabled)
    {
        foreach (MeshRenderer r in renderers)
        {
            r.enabled = enabled;
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine("FireAttack");
        }
    }

    private IEnumerator FireAttack()
    {
        //Rotate half the way around (start at one side)
        isAttacking = true;
        transform.RotateAround(origin.transform.position, Vector3.up, rotateAmount/-2f);
        SetRenderers(rends, true);
        Utils.setCollision(this.gameObject, true);

        for (float angle = 0; angle < rotateAmount; angle += step)
        {
            transform.RotateAround(origin.transform.position, Vector3.up, step);
            yield return new WaitForFixedUpdate();
        }
        //Reset
        transform.localPosition = origPos;
        transform.localRotation = origRot;
        SetRenderers(rends, false);
        Utils.setCollision(this.gameObject, false);
        isAttacking = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Enemy e = other.gameObject.GetComponentInChildren<Enemy>();
                e.takeDamage(damage);
                break;
            // OP Below
            //case "Bullet":
            //    Destroy(other.gameObject);
            //    break;
        }
    }

}
