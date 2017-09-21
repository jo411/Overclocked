using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShadows : MonoBehaviour {

    public GameObject shadowPrefab;
    public float dropRateMultiplier = 1f;
    public bool obeyTimescale = true;

    public TimeScale timeScale;

	// Use this for initialization
	void Start ()
    {
        timeScale = Utils.getTimeScale();
	}

    public void OnSlowTime()
    {
        SpawnShadows();
    }

    public void OnAccelerateTime()
    {
        SpawnShadows();
    }

    public void SpawnShadows()
    {
        CancelInvoke();
        if (timeScale.getScale() < 1f)
        {
            Debug.Log("Beginning to spawn shadows...");
            InvokeRepeating("DropShadow", timeScale.getScale() * dropRateMultiplier, timeScale.getScale() * dropRateMultiplier);
        }
    }

    public void DropShadow()
    {
        GameObject.Instantiate(shadowPrefab, transform.position, transform.rotation);
    }
}
