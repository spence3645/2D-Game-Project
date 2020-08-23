using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : AIController
{

    public List<GameObject> weapons;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        handPos = this.transform.Find("Hand Position");
        ai_rigid = this.GetComponent<Rigidbody2D>();
        enemyHealth = this.GetComponent<EnemyHealth>();
        //dirtTrail = this.transform.Find("Dirt Trail").gameObject;

        scaredDistance = Random.Range(3f, 6f);
        agroDistance = 400;
        shootDistance = 20;
        speed = 1000;
        jumpHeight = 50;

        WeaponChoice();
        StartLook();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(this.transform.position, player.transform.position) >= agroDistance)
        {
            //Do nothing
            isActive = false;
        }
        else if (Vector2.Distance(this.transform.position, player.transform.position) > scaredDistance && Vector2.Distance(this.transform.position, player.transform.position) < agroDistance)
        {
            //Check if the enemy should start shooting
            if (Vector2.Distance(this.transform.position, player.transform.position) < shootDistance)
            {
                isActive = true;
            }
            Movement(1);
        }
        else if(Vector2.Distance(this.transform.position, player.transform.position) < scaredDistance)
        {
            Movement(-1);
        }
        else
        {
            ai_rigid.velocity = new Vector2(0.0f, ai_rigid.velocity.y);
        }
    }

    void FixedUpdate()
    {
        ai_rigid.velocity = new Vector2(Mathf.Clamp(ai_rigid.velocity.x, -6f, 6f), ai_rigid.velocity.y);

        if (isBlocked)
        {
            Jump();
        }
    }

    void WeaponChoice()
    {
        int roll = Random.Range(0, weapons.Count);

        Instantiate(weapons[roll], handPos.position, Quaternion.identity, this.transform);
    }
}
