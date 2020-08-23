using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePump : WeaponBehavior
{

    public GameObject baseStockPrefab;
    public GameObject precisionStockPrefab;
    public GameObject scopePrefab;
    public GameObject barrelPrefab;

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

        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Destroy(this.transform.Find("Barrel").gameObject);
            Instantiate(barrelPrefab, this.transform.Find("Barrel Slot"));
            gunBarrel = this.transform.Find("Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject; //Gun barrel must be added first
            projectileSpeed += 500;
            accuracy += 0.1f;
            rarity += 1;
            hasLongBarrel = true;
        }
        else
        {
            gunBarrel = this.transform.Find("Barrel").gameObject;
        }

        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(scopePrefab, this.transform.Find("Scope Slot"));
            accuracy += 0.2f;
            rarity += 1;
            hasScope = true;
        }

        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(precisionStockPrefab, this.transform.Find("Stock Slot"));
            reloadSpeed -= 0.5f;
            rarity += 1;
            hasPrecisionStock = true;
        }
        else
        {
            Instantiate(baseStockPrefab, this.transform.Find("Stock Slot"));
        }

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "Pump";
        fireRate = Random.Range(1.5f, 2f);
        accuracy = Random.Range(0.5f, 0.6f);
        reloadSpeed = Random.Range(1.5f, 2f);
        damage = 10;
        projectileSpeed = 1000f;
        numOfProjectiles = 5;
        magazineSize = 8;
        magazineTracker = magazineSize;
    }
}
