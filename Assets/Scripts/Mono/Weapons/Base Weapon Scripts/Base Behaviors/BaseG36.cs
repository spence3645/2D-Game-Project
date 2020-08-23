using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseG36 : WeaponBehavior
{
    public GameObject gripPrefab;
    public GameObject underbarrelPrefab;
    public GameObject baseStockPrefab;
    public GameObject ironSightPrefab;
    public GameObject scopePrefab;
    public GameObject precisionStockPrefab;

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
                accuracy += 0.1f;
                rarity += 1;
                hasGrip = true;
            }
            else if (roll > chance / 2 && roll <= chance)
            {
                Instantiate(underbarrelPrefab, this.transform.Find("Rapid Fire Slot"));
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
            accuracy += 0.15f;
            rarity += 1;
            hasScope = true;
        }
        else
        {
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot")); //If no scope, add the iron sights
        }

        //roll for stock
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

        gunBarrel = this.transform.Find("Barrel").gameObject;

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "G36";
        fireRate = Random.Range(0.08f, 0.11f);
        accuracy = Random.Range(0.55f, 0.65f);
        reloadSpeed = Random.Range(1.3f, 1.7f);
        damage = 6;
        projectileSpeed = 1000f;
        magazineSize = 30;
        magazineTracker = magazineSize;
    }
}

