using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpField : MonoBehaviour
{
    private float[] factors = new float[] {.125f,.25f,.5f,1f,2f };
    public float warpFactor = 1f;
    public float warpRadius = 1f;//this should be a power of 2 for float reasons

    public GameObject indicator;
    private SphereCollider field;
    // Use this for initialization
    void Start()
    {
        indicator = Instantiate(indicator,gameObject.transform);
        indicator.transform.position = gameObject.transform.position;

        field = gameObject.AddComponent<SphereCollider>();
        field.isTrigger = true;
        field.radius = warpRadius;

        indicator.transform.localScale = new Vector3(warpRadius*2, .001f, warpRadius*2);
                    
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Modifying");
        other.gameObject.SendMessage("changeTimeScale", warpFactor,SendMessageOptions.DontRequireReceiver);
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Reverting");
        other.gameObject.SendMessage("changeTimeScale", 1/warpFactor, SendMessageOptions.DontRequireReceiver);
    }
   
}
