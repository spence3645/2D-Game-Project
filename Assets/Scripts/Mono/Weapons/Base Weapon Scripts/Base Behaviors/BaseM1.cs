using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseM1 : WeaponBehavior
{

    public GameObject gripPrefab;
    public GameObject baseStockPrefab;
    public GameObject ironSightPrefab;
    public GameObject scopePrefab;
    public GameObject barrelPrefab;
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

        //Roll for grip
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(gripPrefab, this.transform.Find("Grip Slot"));
            accuracy += 0.1f;
            rarity += 1;
            hasGrip = true;
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

        //Roll for stock
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

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "M1";
        fireRate = Random.Range(0.3f, 0.35f);
        accuracy = Random.Range(0.7f, 0.8f);
        reloadSpeed = Random.Range(2f, 2.5f);
        damage = 9;
        projectileSpeed = 1000f;
        magazineSize = 10;
        magazineTracker = magazineSize;
    }
}
