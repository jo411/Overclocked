using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUp : MonoBehaviour {

    // Editing this in the physics collision engine for now. If we ever want enemies to be able to get pickups, we may need to add this back in...
    //public int[] layers;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider c)
    {
        //if (layers.Contains<int>(c.gameObject.layer))
        //{
        //}
        ApplyEffect(c.gameObject);
        Destroy(this.gameObject);
    }

    protected virtual void ApplyEffect(GameObject target) {}
}
