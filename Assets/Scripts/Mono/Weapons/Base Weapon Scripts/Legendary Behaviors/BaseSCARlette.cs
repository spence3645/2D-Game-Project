using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSCARlette : WeaponBehavior
{
    public GameObject gripPrefab;
    public GameObject underbarrelPrefab;
    public GameObject baseStockPrefab;
    public GameObject ironSightPrefab;
    public GameObject scopePrefab;
    public GameObject baseMagazinePrefab;
    public GameObject extendedMagazinePrefab;

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

    void FixedUpdate()
    {

    }

    public override void CreateGun(float chance)
    {
        GunStats();

        rarity += 10;

        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            if(roll <= chance / 2)
            {
                Instantiate(gripPrefab, this.transform.Find("Grip Slot"));
                accuracy += 0.1f;
                rarity += 1;
                hasGrip = true;
            }
            else if(roll > chance / 2 && roll <= chance)
            {
                Instantiate(underbarrelPrefab, this.transform.Find("Underbarrel Slot"));
                fireRate -= 0.08f;
                rarity += 1;
                hasRapid = true;
            }
        }

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
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot")); //If no scope, add the iron sights
        }

        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(extendedMagazinePrefab, this.transform.Find("Extended Magazine Slot"));
            magazineSize += 10;
            rarity += 1;
            hasExtended = true;
        }
        else
        {
            Instantiate(baseMagazinePrefab, this.transform.Find("Base Magazine Slot"));
        }

        Instantiate(baseStockPrefab, this.transform.Find("Base Stock Slot"));
        gunBarrel = this.transform.Find("Barrel").gameObject;

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "SCARlette";
        fireRate = Random.Range(0.15f, 0.18f);
        accuracy = Random.Range(0.6f, 0.7f);
        reloadSpeed = Random.Range(1.2f, 1.7f);
        damage = 15;
        projectileSpeed = 1000f;
        numOfProjectiles = 1;
        magazineSize = 25;
        magazineTracker = magazineSize;
    }
}
