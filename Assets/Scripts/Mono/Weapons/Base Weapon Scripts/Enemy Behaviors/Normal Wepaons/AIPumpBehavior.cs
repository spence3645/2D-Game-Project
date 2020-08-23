using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPumpBehavior : AIWeaponBehavior
{

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunBarrel = this.transform.Find("Barrel").gameObject;
        bulletType = this.transform.Find("Bullet").gameObject;
        aiController = this.transform.GetComponentInParent<AIController>();

        fireRate = Random.Range(1.2f, 1.5f);
        accuracy = Random.Range(0.6f, 0.8f);
        reloadTime = Random.Range(3f, 3.5f);
        damage = 3;
        projectileSpeed = 650f;
        numOfProjectiles = 5;
        magazineSize = 5;
        magazineTracker = magazineSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aiController.isActive)
        {
            Aiming(player.transform.position);
            CheckFire();
        }

        CheckReload();
    }

    //Copies equipped weapon and changes velocity relative to normalized mouse vector and projectile speed
    //TODO Projectile isn't launching directly in line with the mouse cursor
    public override void Fire()
    {
        for (int i = 0; i < numOfProjectiles; i++)
        {
            GameObject bulletClone = Instantiate(bulletType, gunBarrel.transform.position, Quaternion.identity);

            //Adds accuracy to send bullets off path
            float offsetRange = 1 - accuracy;
            float bulletOffsetX = Random.Range(-offsetRange, offsetRange);
            float bulletOffsetY = Random.Range(-offsetRange, offsetRange);
            bulletClone.transform.right = aimDirection + new Vector2(bulletOffsetX, bulletOffsetY);

            bulletClone.GetComponent<SpriteRenderer>().enabled = true;
            bulletClone.GetComponent<CircleCollider2D>().enabled = true;

            if (aiController.isFacingRight)
            {
                bulletClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletClone.transform.right.x * projectileSpeed, bulletClone.transform.right.y * projectileSpeed));
            }
            else
            {
                bulletClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bulletClone.transform.right.x * -projectileSpeed, -bulletClone.transform.right.y * -projectileSpeed));
            }
        }
    }
}
