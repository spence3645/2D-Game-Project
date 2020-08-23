using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseP90 : WeaponBehavior
{
   
    public GameObject ironSightPrefab;
    public GameObject scopePrefab;
    public GameObject reflexPrefab;
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
            if (roll <= chance / 2)
            {
                Instantiate(scopePrefab, this.transform.Find("Scope Slot"));
                accuracy += 0.10f;
                rarity += 1;
                hasScope = true;
            }
            else if (roll > chance / 2 && roll <= chance)
            {
                Instantiate(reflexPrefab, this.transform.Find("Relex Sight Slot"));
                hasReflex = true;
            }
        }
        else
        {
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot")); //If no scope, add the iron sights
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
        weaponModel = "P90";
        fireRate = Random.Range(0.075f, 0.09f);
        accuracy = Random.Range(0.7f, 0.75f);
        reloadSpeed = Random.Range(2f, 2.5f);
        damage = 4;
        projectileSpeed = 1000f;
        magazineSize = 50;
        magazineTracker = magazineSize;
    }
}
