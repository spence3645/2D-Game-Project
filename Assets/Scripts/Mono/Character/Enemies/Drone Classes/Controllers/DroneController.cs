using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : AIController
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ai_rigid = this.GetComponent<Rigidbody2D>();
        enemyHealth = this.GetComponent<EnemyHealth>();

        scaredDistance = Random.Range(6f, 8f);
        agroDistance = 400;
        shootDistance = 20;
        speed = 1000;
        jumpHeight = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) >= agroDistance)
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
        else if (Vector2.Distance(this.transform.position, player.transform.position) < scaredDistance)
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

    public override void Movement(int direction)
    {
        //moves character on horizontal axis
        Vector2 playerDirection = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y).normalized;
        playerDirection *= direction * (speed * Time.deltaTime);
        ai_rigid.AddForce(playerDirection);

        if (playerDirection.x == 0)
        {
            ai_rigid.velocity = new Vector2(0.0f, ai_rigid.velocity.y);
        }
    }
}
