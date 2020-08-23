using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAK : WeaponBehavior
{

    public GameObject gripPrefab;
    public GameObject stockPrefab;
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

    public override void CreateGun(float chance)
    {
        GunStats();

        float roll = Random.Range(0f, 1f);
        if(roll <= chance)
        {
            Instantiate(gripPrefab, this.transform.Find("Grip Slot"));
            accuracy += 0.1f;
            rarity += 1;
            hasGrip = true;
        }

        roll = Random.Range(0f, 1f);
        if(roll <= chance)
        {
            Instantiate(scopePrefab, this.transform.Find("Scope Slot"));
            accuracy += 0.2f;
            rarity += 1;
            hasScope = true;
        }

        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(stockPrefab, this.transform.Find("Stock Slot"));
            accuracy += 0.1f;
            rarity += 1;
            hasPrecisionStock = true;
        }

        Instantiate(barrelPrefab, this.transform.Find("Barrel Slot"));

        gunBarrel = this.transform.Find("Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject; //Gun barrel must be added first

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "AK";
        fireRate = Random.Range(0.12f, 0.15f);
        accuracy = Random.Range(0.5f, 0.6f);
        reloadSpeed = Random.Range(1.5f, 2f);
        damage = 6;
        projectileSpeed = 900f;
        numOfProjectiles = 1;
        magazineSize = 30;
        magazineTracker = magazineSize;
    }
}
