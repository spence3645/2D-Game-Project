using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAA12 : WeaponBehavior
{
    public GameObject baseStockPrefab;
    public GameObject precisionStockPrefab;
    public GameObject scopePrefab;
    public GameObject ironSightPrefab;
    public GameObject barrelPrefab;
    public GameObject underBarrelPrefab;
    public GameObject gripPrefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), bulletType.GetComponent<CircleCollider2D>());

        CheckFire();

        CheckReload();

        CheckAim();
    }

    public override void Fire()
    {
        magazineTracker--;

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

    public override void CreateGun(float chance)
    {
        GunStats();

        //Grip roll
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            if (roll <= chance / 2)
            {
                Instantiate(gripPrefab, this.transform.Find("Grip Slot"));
                accuracy += 0.1f;
                rarity += 1;
                hasGrip = true;
            }
            else if (roll > chance / 2 && roll <= chance)
            {
                Instantiate(underBarrelPrefab, this.transform.Find("Rapid Fire Slot"));
                fireRate -= 0.05f;
                rarity += 1;
                hasRapid = true;
            }
        }
        //scope roll
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(scopePrefab, this.transform.Find("Scope Slot"));
            accuracy += 0.2f;
            rarity += 1;
            hasScope = true;
        }
        else
        {
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot"));
        }
        //stock roll
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(precisionStockPrefab, this.transform.Find("Precision Stock Slot"));
            reloadSpeed -= 0.5f;
            rarity += 1;
            hasPrecisionStock = true;
        }
        else 
        {
            Instantiate(baseStockPrefab, this.transform.Find("Base Stock Slot"));
        }

        Instantiate(barrelPrefab, this.transform.Find("Barrel Slot"));

        gunBarrel = this.transform.Find("Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject; //Gun barrel must be added first

        ChooseColor(); //Pick the rarity color based on the rarity number
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "A12";
        fireRate = Random.Range(.14f, .17f);
        accuracy = Random.Range(0.48f, 0.58f);
        reloadSpeed = Random.Range(1.5f, 1.7f);
        damage = 6;
        projectileSpeed = 1000f;
        numOfProjectiles = 5;
        magazineSize = 12;
        magazineTracker = magazineSize;
    }
}
