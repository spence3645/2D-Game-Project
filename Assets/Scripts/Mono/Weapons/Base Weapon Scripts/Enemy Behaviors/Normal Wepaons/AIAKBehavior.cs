using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAKBehavior : AIWeaponBehavior
{

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunBarrel = this.transform.Find("Barrel").gameObject;
        bulletType = this.transform.Find("Bullet").gameObject;
        aiController = this.transform.GetComponentInParent<AIController>();

        fireRate = Random.Range(0.2f, 0.22f);
        accuracy = Random.Range(0.6f, 0.8f);
        reloadSpeed = Random.Range(0.8f, 1f);
        damage = 4;
        projectileSpeed = 650f;
        magazineSize = 10;
        magazineTracker = magazineSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
