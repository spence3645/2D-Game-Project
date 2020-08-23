using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject player;

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = this.transform.position;
        }
        else
        {
            GameObject player = Instantiate(playerPrefab, this.transform.position, Quaternion.identity, null);
            player.name = player.name.Replace("(Clone)", "");
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
