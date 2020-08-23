using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScar : WeaponBehavior
{

    public GameObject gripPrefab;
    public GameObject baseStockPrefab;
    public GameObject ironSightPrefab;
    public GameObject scopePrefab;
    public GameObject barrelPrefab;
    public GameObject highDamageBarrelPrefab;

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

    public override void CreateGun(float chance)
    {
        GunStats();

        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(gripPrefab, this.transform.Find("Grip Slot"));
            accuracy += 0.1f;
            rarity += 1;
            hasGrip = true;
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
            Instantiate(highDamageBarrelPrefab, this.transform.Find("High Damage Barrel Slot"));
            gunBarrel = this.transform.Find("High Damage Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject; //Gun barrel must be added first
            damage += 1;
            rarity += 1;
            hasFatBarrel = true;
        }
        else
        {
            Instantiate(barrelPrefab, this.transform.Find("Base Barrel Slot"));
            gunBarrel = this.transform.Find("Base Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject;
        }

        Instantiate(baseStockPrefab, this.transform.Find("Stock Slot"));
        Instantiate(barrelPrefab, this.transform.Find("Barrel Slot"));

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "SCAR";
        fireRate = Random.Range(0.15f, 0.18f);
        accuracy = Random.Range(0.6f, 0.7f);
        reloadSpeed = Random.Range(1.2f, 1.7f);
        damage = 7;
        projectileSpeed = 1000f;
        numOfProjectiles = 1;
        magazineSize = 25;
        magazineTracker = magazineSize;
    }
}
