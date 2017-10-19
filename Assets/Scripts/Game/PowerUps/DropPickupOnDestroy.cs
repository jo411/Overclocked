using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class pickupRecord
{
    public GameObject prefab;
    public float dropChance;
}

public class DropPickupOnDestroy : MonoBehaviour {

    public pickupRecord[] pickups;
    public bool disableDrops = false;
    	// Use this for initialization
	void Start () {
        pickups = pickups.OrderBy(x => x.dropChance).ToArray();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnApplicationQuit()
    {
        disableDrops = true;
    }

    void OnDestroy()
    {
        if (!disableDrops)
        {
            foreach(pickupRecord current in pickups)
            {
                if(Utils.checkProbability(current.dropChance))
                {
                    GameObject pickup = Instantiate(current.prefab, transform.position, Quaternion.identity);//drop pickup
                    break;//if an object spawned then stop checking
                }

            }
        }
    }
    
}



