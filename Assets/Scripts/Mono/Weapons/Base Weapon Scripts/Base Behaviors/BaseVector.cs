using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVector : WeaponBehavior
{

    public GameObject gripPrefab;
    public GameObject baseStockPrefab;
    public GameObject precisionStockPrefab;
    public GameObject ironSightPrefab;
    public GameObject scopePrefab;
    public GameObject barrelPrefab;
    public GameObject extendedMagazine;

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

        //Roll for grip
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(gripPrefab, this.transform.Find("Grip Slot"));
            accuracy += 0.15f;
            rarity += 1;
            hasGrip = true;
        }

        //Roll for scope
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(scopePrefab, this.transform.Find("Scope Slot"));
            accuracy += 0.15f;
            rarity += 1;
            hasScope = true;
        }
        else
        {
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot")); //If no scope, add the iron sights
        }

        //Roll for barrel
        roll = Random.Range(0f, 1f);
        if(roll <= chance)
        {
            Destroy(this.transform.Find("Barrel").gameObject);
            Instantiate(barrelPrefab, this.transform.Find("Barrel Slot"));
            gunBarrel = this.transform.Find("Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject; //Gun barrel must be added first
            projectileSpeed += 500;
            rarity += 1;
            hasLongBarrel = true;
        }
        else
        {
            gunBarrel = this.transform.Find("Barrel").gameObject;
        }

        //Roll for stock
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(precisionStockPrefab, this.transform.Find("Stock Slot"));
            reloadSpeed -= 0.5f;
            accuracy += 0.05f;
            rarity += 1;
            hasPrecisionStock = true;
        }
        else
        {
            Instantiate(baseStockPrefab, this.transform.Find("Stock Slot"));
        }

        //Roll for magazine
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(extendedMagazine, this.transform.Find("Magazine Slot"));
            reloadSpeed += 0.25f;
            magazineSize += 20;
            magazineTracker = magazineSize;
            rarity += 1;
            hasExtended = true;
        }

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "Vector";
        fireRate = Random.Range(0.06f, 0.08f);
        accuracy = Random.Range(0.5f, 0.6f);
        reloadSpeed = Random.Range(1.5f, 2f);
        damage = 3;
        projectileSpeed = 1000f;
        magazineSize = 20;
        magazineTracker = magazineSize;
    }
}
