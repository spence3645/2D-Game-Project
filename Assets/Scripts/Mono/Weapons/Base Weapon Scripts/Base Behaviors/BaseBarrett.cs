using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBarrett : WeaponBehavior
{
    public GameObject baseStockPrefab;
    public GameObject underbarrelPrefab;
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
        rarity += 2;

        //Roll for underbarrel
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(underbarrelPrefab, this.transform.Find("Rapid Fire Slot"));
            fireRate -= 0.05f;
            rarity += 1;
            hasRapid = true;
        }

        //Roll for scope
        roll = Random.Range(0f, 1f);
        if (roll <= chance/2)
        {
            Instantiate(scopePrefab, this.transform.Find("Scope Slot"));
            accuracy += 0.15f;
            rarity += 1;
            hasScope = true;
        }

        Instantiate(baseStockPrefab, this.transform.Find("Stock Slot"));
        Instantiate(barrelPrefab, this.transform.Find("Base Barrel Slot"));
        gunBarrel = this.transform.Find("Base Barrel Slot").transform.GetChild(0).transform.GetChild(0).gameObject;

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = ".50 Cal";
        fireRate = Random.Range(1f, 1.5f);
        accuracy = Random.Range(0.8f, 0.85f);
        reloadSpeed = Random.Range(1.5f, 2f);
        damage = 25;
        projectileSpeed = 1000f;
        magazineSize = 5;
        magazineTracker = magazineSize;
    }
}

