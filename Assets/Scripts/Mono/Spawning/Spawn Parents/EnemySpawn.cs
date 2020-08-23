using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public List<GameObject> enemy = new List<GameObject>();
    public EnemyTracker enemyTracker;

    public bool isSpawning;
    public bool atHub;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Spawn(Vector3 loc)
    {
        
    }
}
