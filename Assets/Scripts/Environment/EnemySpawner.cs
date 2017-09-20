using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyPrefab;

    private bool canSpawn = true;
    private float timeSinceSpawn = 0f;
    private TimeScale timeScale;

    [SerializeField]
    public float spawnRate = 10f;

    //Spawn rate delayed by time scale?
    [SerializeField]
    private bool spawnDelayedByTimeScale = false;

    // Use this for initialization
    void Start () 
    {
        timeScale = Utils.getTimeScale();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (!canSpawn)
        {
            if (spawnDelayedByTimeScale)
            {
                timeSinceSpawn += Time.deltaTime * getTimeScale();
            }
            else
            {
                timeSinceSpawn += Time.deltaTime;
            }
            if (timeSinceSpawn >= spawnRate)
            {
                //If we ever wanted to control spawn rate with time.
                canSpawn = true;
            }
        }
        else
        {
            SpawnEnemy();
        }
    }

    public virtual void SpawnEnemy()
    {
        if (canSpawn)
        {
            GameObject enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            canSpawn = false;
            timeSinceSpawn = 0f;
        }
    }

    protected float getTimeScale()//TODO: not sure if weapons have colliders, maybe use send message to children when altering personal timescales
    {
        return timeScale.getScale();
    }

}
