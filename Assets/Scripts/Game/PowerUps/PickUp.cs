using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public int[] layers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider c)
    {
        if (layers.Contains<int>(c.gameObject.layer))
        {
            ApplyEffect(c.gameObject);
            Destroy(this.gameObject);
        }
    }

    protected virtual void ApplyEffect(GameObject target) {}
}
