using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVectorBehavior : AIWeaponBehavior
{

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunBarrel = this.transform.Find("Barrel").gameObject;
        bulletType = this.transform.Find("Bullet").gameObject;
        aiController = this.transform.GetComponentInParent<AIController>();

        fireRate = Random.Range(0.06f, 0.08f);
        accuracy = Random.Range(0.4f, 0.6f);
        reloadSpeed = Random.Range(0.8f, 1f);
        damage = 2;
        projectileSpeed = 650f;
        magazineSize = 5;
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
