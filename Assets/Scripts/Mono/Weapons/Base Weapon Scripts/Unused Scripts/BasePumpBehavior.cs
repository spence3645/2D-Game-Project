using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//High damage, low rate of fire, wide spread
public class BasePumpBehavior : WeaponBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        projectileSpeed = 1000f;
        numOfProjectiles = 5;
        magazineSize = 8;
        magazineTracker = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), bulletType.GetComponent<CircleCollider2D>());

        CheckFire();

        CheckReload();

        CheckAim();
    }

    void FixedUpdate()
    {

    }

    //Copies equipped weapon and changes velocity relative to normalized mouse vector and projectile speed
    //TODO Projectile isn't launching directly in line with the mouse cursor
    public override void Fire()
    {
        for(int i = 0; i < numOfProjectiles; i++)
        {
            GameObject bulletClone = Instantiate(bulletType, gunBarrel.transform.position, Quaternion.identity);

            //Adds accuracy to send bullets off path
            float offsetRange = 1 - accuracy;
            float bulletOffsetX = Random.Range(-offsetRange, offsetRange);
            float bulletOffsetY = Random.Range(-offsetRange, offsetRange);
            bulletClone.transform.right = aimDirection + new Vector2(bulletOffsetX, bulletOffsetY);

            bulletClone.GetComponent<SpriteRenderer>().enabled = true;
            bulletClone.GetComponent<CircleCollider2D>().enabled = true;

            if (characterController.isFacingRight)
            {
                bulletClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletClone.transform.right.x * projectileSpeed, bulletClone.transform.right.y * projectileSpeed));
            }
            else
            {
                bulletClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bulletClone.transform.right.x * -projectileSpeed, -bulletClone.transform.right.y * -projectileSpeed));
            }
        }
    }

    public override void WhiteStats()
    {
        fireRate = Random.Range(2.5f, 3f);
        accuracy = Random.Range(0.5f, 0.6f);
        reloadSpeed = Random.Range(1.5f, 2f);
        damage = 5;
    }

    public override void GreenStats()
    {
        fireRate = Random.Range(2f, 2.5f);
        accuracy = Random.Range(0.6f, 0.7f);
        reloadSpeed = Random.Range(1.2f, 1.5f);
        damage = 6;
    }

    public override void BlueStats()
    {
        fireRate = Random.Range(1.6f, 2f);
        accuracy = Random.Range(0.7f, 0.8f);
        reloadSpeed = Random.Range(1f, 1.2f);
        damage = 7;
    }
}
