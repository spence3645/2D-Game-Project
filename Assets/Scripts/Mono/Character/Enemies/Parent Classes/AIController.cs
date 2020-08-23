using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public int jumpHeight; //Needs to be bigger since jumping is a force not position
    public int agroDistance;
    public int shootDistance;
    public float scaredDistance;
    public int speed;

    public bool isGrounded;
    public bool isFacingRight; //Public so EquippedWeapon can access to decide how to aim the gun
    public bool isBlocked;
    public bool isActive;

    public GameObject player;
    public GameObject dirtTrail;

    public Transform handPos;

    public Rigidbody2D ai_rigid;

    public EnemyHealth enemyHealth;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.collider);
        }
    }

    public virtual void Movement(int direction)
    {
        //moves character on horizontal axis
        Vector2 playerDirection = new Vector2(player.transform.position.x - this.transform.position.x, 0).normalized;
        playerDirection *= direction*(speed * Time.deltaTime);
        ai_rigid.AddForce(playerDirection);

        if (playerDirection.x > 0 && !isFacingRight)
        {
            Vector3 localScale = this.transform.localScale;
            localScale.x = 1;
            this.transform.localScale = localScale;
            isFacingRight = true;
        }

        else if (playerDirection.x < 0 && isFacingRight)
        {
            Vector3 localScale = this.transform.localScale;
            localScale.x = -1;
            this.transform.localScale = localScale;
            isFacingRight = false;
        }

        if (playerDirection.x == 0)
        {
            ai_rigid.velocity = new Vector2(0.0f, ai_rigid.velocity.y);
        }
    }

    public void Jump()
    {
        Vector2 tempVector = new Vector2(0, jumpHeight);
        ai_rigid.AddForce(tempVector);
    }

    public void StartLook()
    {
        if (this.transform.localScale.x == 1)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }
    }
}
