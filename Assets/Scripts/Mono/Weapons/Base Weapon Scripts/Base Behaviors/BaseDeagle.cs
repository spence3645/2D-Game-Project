using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDeagle : WeaponBehavior

{
    public GameObject ironSightPrefab;
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
        rarity += 1;
    

        //Roll for scope
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot"));
            accuracy += 0.05f;
            rarity += 1;
            hasScope = true;
        }
       

        //Roll for barrel
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Destroy(this.transform.Find("Barrel").gameObject);
            Instantiate(silencerPrefab, this.transform.Find("Silencer Slot"));
            gunBarrel = this.transform.Find("Silencer Slot").transform.GetChild(0).transform.GetChild(0).gameObject; //Gun barrel must be added first
            projectileSpeed += 500;
            rarity += 1;
            hasSilencer = true;
        }
        else
        {
            gunBarrel = this.transform.Find("Barrel").gameObject;
        }

        ChooseColor();
        NameWeapon();
    }

    public override void GunStats()
    {
        weaponModel = "Deagle";
        fireRate = Random.Range(0.2f, 0.25f);
        accuracy = Random.Range(0.7f, 0.75f);
        reloadSpeed = Random.Range(1f, 1.4f);
        damage = 12;
        projectileSpeed = 1000f;
        magazineSize = 12;
        magazineTracker = magazineSize;
    }
}

