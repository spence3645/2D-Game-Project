using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSpawn : EnemySpawn
{
    void Start()
    {
        atHub = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //TODO, these parameters only work for the test scene!!!!!
        if (scene.name.Contains("Zone"))
        {
            enemyTracker = GameObject.Find("Grid").GetComponent<EnemyTracker>();
            enemyTracker.TakeCount();
            atHub = false;
        }
        else if (scene.name == "Hub")
        {
            atHub = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning && !atHub && enemyTracker.enemies.Count <= 8)
        {
            isSpawning = true;
            StartCoroutine(StartSpawn(this.transform.position));
        }
    }

    void FixedUpdate()
    {
        if (enemyTracker)
        {
            enemyTracker.TakeCount();
        }
    }

    IEnumerator StartSpawn(Vector3 loc)
    {
        Spawn(loc);
        yield return new WaitForSeconds(Random.Range(4f, 6f));
        isSpawning = false;
    }

    /*
     * 0 - Bandit Soldier
     * 1 - Drones
     * 2 - Seargent Bandit
     */
    public override void Spawn(Vector3 loc)
    {
        Vector3 spawnCameraLoc = Camera.main.WorldToViewportPoint(loc);

        if (spawnCameraLoc.x > 1 || spawnCameraLoc.x < 0)
        {
            float roll = Random.Range(0f, 1f);

            if (roll > 0.3f)
            {
                GameObject spawnedEnemy = Instantiate(enemy[0], new Vector3(loc.x, loc.y, 0), Quaternion.identity);
                spawnedEnemy.name = spawnedEnemy.name.Replace("(Clone)", "");
            }

            else if(roll > 0.005f && roll <= 0.3f)
            {
                GameObject spawnedEnemy = Instantiate(enemy[1], new Vector3(loc.x, loc.y, 0), Quaternion.identity);
                spawnedEnemy.name = spawnedEnemy.name.Replace("(Clone)", "");
            }

            else if (roll <= 0.005f)
            {
                GameObject spawnedEnemy = Instantiate(enemy[2], new Vector3(loc.x, loc.y, 0), Quaternion.identity);
                spawnedEnemy.name = spawnedEnemy.name.Replace("(Clone)", "");
            }
        }
    }
}
