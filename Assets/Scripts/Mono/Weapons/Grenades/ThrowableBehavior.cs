using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBehavior : MonoBehaviour
{

    public float fireRate;
    public int numOfProjectiles;
    public float projectileSpeed;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = Random.Range(.1f, 1f);
        projectileSpeed = Random.Range(5f, 10f);
        numOfProjectiles = 1; //Temporary
        player = GameObject.FindGameObjectWithTag("Player");

        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
