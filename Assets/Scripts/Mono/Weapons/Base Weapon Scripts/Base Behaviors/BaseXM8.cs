using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseXM8 : WeaponBehavior
{
    public GameObject gripPrefab;
    public GameObject rapidFirePrefab;
    public GameObject baseStockPrefab;
    public GameObject ironSightPrefab;
    public GameObject scopePrefab;
    public GameObject barrelPrefab;
    public GameObject fatBarrelPrefab;
    public GameObject drumMagazinePrefab;
    public GameObject baseMagazinePrefab;

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

        //Roll for underbarrel
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            if (roll <= chance / 2)
            {
                Instantiate(gripPrefab, this.transform.Find("Grip Slot"));
                accuracy += 0.05f;
                rarity += 1;
                hasGrip = true;
            }
            else if (roll > chance / 2 && roll <= chance)
            {
                Instantiate(rapidFirePrefab, this.transform.Find("Rapid Fire Slot"));
                fireRate -= 0.05f;
                rarity += 1;
                hasRapid = true;
            }
        }

        //Roll for scope
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(scopePrefab, this.transform.Find("Scope Slot"));
            accuracy += 0.1f;
            rarity += 1;
            hasScope = true;
        }
        else
        {
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot")); //If no scope, add the iron sights
        }

        //Roll for barrel
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(fatBarrelPrefab, this.transform.Find("Fat Barrel Slot"));
            gunBarrel = this.transform.Find("Fat Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject; //Gun barrel must be added first
            projectileSpeed += 500;
            rarity += 1;
            hasFatBarrel = true;
        }
        else
        {
            Instantiate(barrelPrefab, this.transform.Find("Base Barrel Slot"));
            gunBarrel = this.transform.Find("Base Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject;
        }

     
        //Roll for magazine
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(drumMagazinePrefab, this.transform.Find("Drum Magazine Slot"));
            reloadSpeed += 0.5f;
            magazineSize += 70;
            magazineTracker = magazineSize;
            rarity += 1;
            hasExtended = true;
        }
        else
        {
            Instantiate(baseMagazinePrefab, this.transform.Find("Base Magazine Slot"));
        }

        Instantiate(baseStockPrefab, this.transform.Find("Base Stock Slot"));

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "XM8";
        fireRate = Random.Range(0.11f, 0.14f);
        accuracy = Random.Range(0.75f, 0.8f);
        reloadSpeed = Random.Range(1.2f, 1.5f);
        damage = 6;
        projectileSpeed = 1100f;
        magazineSize = 30;
        magazineTracker = magazineSize;
    }
}
