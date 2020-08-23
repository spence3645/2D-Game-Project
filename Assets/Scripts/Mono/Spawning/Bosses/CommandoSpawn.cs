using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoSpawn : EnemySpawn
{
    // Start is called before the first frame update
    void Awake()
    {
        enemyTracker = GameObject.Find("Grid").GetComponent<EnemyTracker>();
        Spawn(this.transform.position);
        enemyTracker.TakeCount();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        enemyTracker.TakeCount();
    }

    /*
     * 0 - Bandit Soldier
     * 1 - Seargent Bandit
     */
    public override void Spawn(Vector3 loc)
    {
        //Vector3 spawnCameraLoc = Camera.main.WorldToViewportPoint(loc);

        GameObject spawnedEnemy = Instantiate(enemy[0], loc, Quaternion.identity);
        spawnedEnemy.name = spawnedEnemy.name.Replace("(Clone)", "");
    }
}
