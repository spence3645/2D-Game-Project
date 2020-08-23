using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVoidRifle : WeaponBehavior
{
    public GameObject magazinePrefab;
    public GameObject extendedPrefab;
    public GameObject baseStockPrefab;
    public GameObject ironSightPrefab;
    public GameObject reflexSightPrefab;
    public GameObject scopePrefab;

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

    public override void SecondaryFire()
    {
        GameObject bulletClone = Instantiate(secondaryBullet, secondaryBarrel.transform.position, Quaternion.identity);

        bulletClone.transform.right = aimDirection;
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

    public override void CreateGun(float chance)
    {
        GunStats();

        rarity += 10;

        //Roll for underbarrel
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(extendedPrefab, this.transform.Find("Extended Slot"));
            magazineSize += 10;
            rarity += 1;
            hasExtended = true;
        }
        else
        {
            Instantiate(magazinePrefab, this.transform.Find("Magazine Slot"));
        }

        //Roll for scope
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            if(roll <= chance / 2)
            {
                Instantiate(scopePrefab, this.transform.Find("Scope Slot"));
                accuracy += 0.15f;
                rarity += 1;
                hasScope = true;
            }
            else if(roll > chance/2 && roll <= chance)
            {
                Instantiate(reflexSightPrefab, this.transform.Find("Reflex Slot"));
                accuracy += 0.05f;
                rarity += 1;
                hasReflex = true;
            }
        }
        else
        {
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot")); //If no scope, add the iron sights
        }

        Instantiate(baseStockPrefab, this.transform.Find("Stock Slot"));

        gunBarrel = this.transform.Find("Barrel").gameObject;
        secondaryBarrel = this.transform.Find("Secondary Barrel").gameObject;

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "Void Rifle";
        fireRate = Random.Range(0.08f, 0.11f);
        fireRateSecondary = 2f;
        accuracy = Random.Range(0.7f, 0.8f);
        reloadSpeed = Random.Range(1.3f, 1.7f);
        damage = 13;
        projectileSpeed = 1000f;
        magazineSize = 30;
        magazineTracker = magazineSize;
        hasSecondary = true;
    }
}
