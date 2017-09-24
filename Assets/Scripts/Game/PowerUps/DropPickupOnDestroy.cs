using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class pickupRecord
{
    public PickUp prefab;
    public float dropChance;
}

public class DropPickupOnDestroy : MonoBehaviour {

    public pickupRecord[] pickups;
    	// Use this for initialization
	void Start () {
        pickups = pickups.OrderBy(x => x.dropChance).ToArray();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        foreach(pickupRecord current in pickups)
        {
            if(Utils.checkProbability(current.dropChance))
            {
                PickUp pickup = Instantiate(current.prefab);//drop pickup
                pickup.transform.position = transform.position;//set location to this game object's position
                break;//if an object spawned then stop checking
            }

        }
    }
    
}



