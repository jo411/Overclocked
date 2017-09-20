using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public GameObject[] spawnableEnemies;
    public int waveCount = 5;
    public GameObject source;
    public float spawnDistance = 10f;
    public bool spawnAroundPlayer = true;

    private bool canSpawn = true;
    private float timeSinceSpawn = 0f;
    private int enemiesSpawned;
    private TimeScale timeScale;
    private Enemy[] enemyTracker;

    [SerializeField]
    public float spawnRate = 2f;

    //Spawn rate delayed by time scale?
    [SerializeField]
    private bool spawnDelayedByTimeScale = false;

    // Use this for initialization
    void Start () 
    {
        timeScale = Utils.getTimeScale();
        enemiesSpawned = 0;
        enemyTracker = new Enemy[waveCount];
        if (!spawnAroundPlayer) source = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned < waveCount)
        {
            if (canSpawn)
            {
                spawnEnemy();
            }
            else
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
        }
        else
        {
            if(isWaveDead())
            {
                enemiesSpawned = 0;
                canSpawn = false;
            }
        }
	}

    private bool isWaveDead()
    {
        for (int i = 0; i < enemyTracker.Length; i++)
        {
            if (enemyTracker[i] != null) return false;
        }
        return true;
    }

    private void spawnEnemy()
    {
        if (canSpawn)
        {
            Vector2 randCircle = Random.insideUnitCircle;
            Vector3 spawnPos = source.transform.position + new Vector3(randCircle.x, 0, randCircle.y) * spawnDistance;
            GameObject enemy = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length)], spawnPos, Quaternion.identity);
            enemyTracker[enemiesSpawned] = enemy.GetComponentInChildren<Enemy>();
            canSpawn = false;
            timeSinceSpawn = 0f;
            enemiesSpawned++;
        }
    }
    
    protected float getTimeScale()//TODO: not sure if weapons have colliders, maybe use send message to children when altering personal timescales
    {
        return timeScale.getScale();
    }
}
