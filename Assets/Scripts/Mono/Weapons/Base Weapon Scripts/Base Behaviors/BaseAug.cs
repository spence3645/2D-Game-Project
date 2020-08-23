using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAug : WeaponBehavior
{

    public GameObject gripPrefab;
    public GameObject baseStockPrefab;
    public GameObject underbarrelPrefab;
    public GameObject ironSightPrefab;
    public GameObject scopePrefab;
    public GameObject barrelPrefab;
    public GameObject silencerPrefab;

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
            if (roll <= chance/2)
            {
                Instantiate(gripPrefab, this.transform.Find("Grip Slot"));
                accuracy += 0.1f;
                rarity += 1;
                hasGrip = true;
            }
            else if (roll > chance/2 && roll <= chance)
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

        //Roll for barrel
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(silencerPrefab, this.transform.Find("Silencer Slot"));
            gunBarrel = this.transform.Find("Silencer Slot").transform.GetChild(0).transform.GetChild(0).gameObject; //Gun barrel must be added first
            projectileSpeed += 500;
            rarity += 1;
            hasSilencer = true;
        }
        else
        {
            Instantiate(barrelPrefab, this.transform.Find("Base Barrel Slot"));
            gunBarrel = this.transform.Find("Base Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject;
        }

        Instantiate(baseStockPrefab, this.transform.Find("Stock Slot"));
        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "AUG";
        fireRate = Random.Range(0.08f, 0.1f);
        accuracy = Random.Range(0.6f, 0.7f);
        reloadSpeed = Random.Range(1.5f, 2f);
        damage = 5;
        projectileSpeed = 1000f;
        magazineSize = 35;
        magazineTracker = magazineSize;
    }
}
