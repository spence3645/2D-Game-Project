using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAssassinDeagle : WeaponBehavior
{
    public GameObject ironSightPrefab;
    public GameObject scopedPrefab;
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
        rarity += 10;

        //Roll for scope
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            Instantiate(scopedPrefab, this.transform.Find("Scope Slot"));
            accuracy += 0.15f;
            rarity += 1;
            hasScope = true;
        }
        else
        {
            Instantiate(ironSightPrefab, this.transform.Find("Iron Sight Slot"));
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
        weaponModel = "Assassin's Deagle";
        fireRate = Random.Range(0.05f, 0.075f);
        accuracy = Random.Range(0.7f, 0.75f);
        reloadSpeed = Random.Range(0.8f, 1f);
        damage = 15;
        projectileSpeed = 1000f;
        magazineSize = 12;
        magazineTracker = magazineSize;
    }
}
