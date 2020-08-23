using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAssassinDeagle : AIWeaponBehavior
{
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunBarrel = this.transform.Find("Barrel").gameObject;
        bulletType = this.transform.Find("Bullet").gameObject;
        aiController = this.transform.GetComponentInParent<AIController>();

        fireRate = Random.Range(0.1f, 0.12f);
        accuracy = Random.Range(0.6f, 0.8f);
        reloadSpeed = Random.Range(0.8f, 1f);
        damage = 8;
        projectileSpeed = 650f;
        magazineSize = 5;
        magazineTracker = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiController.isActive)
        {
            Aiming(player.transform.position);
            CheckFire();
        }

        CheckReload();
    }
}
