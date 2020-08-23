using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : AIBulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        weaponBehavior = this.GetComponentInParent<AIWeaponBehavior>();

        if (damage == 0)
        {
            damage = weaponBehavior.damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
