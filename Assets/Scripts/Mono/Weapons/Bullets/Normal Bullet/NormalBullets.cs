using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullets : BulletScript
{
    // Start is called before the first frame update
    void Start()
    {
        weaponBehavior = this.GetComponentInParent<WeaponBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damage == 0)
        {
            damage = weaponBehavior.damage;
        }
    }

    void FixedUpdate()
    {

    }
}
