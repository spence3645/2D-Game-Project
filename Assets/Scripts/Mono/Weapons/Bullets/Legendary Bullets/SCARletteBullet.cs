using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCARletteBullet : BulletScript
{
    int bleedDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        weaponBehavior = this.GetComponentInParent<WeaponBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damage == 0)
        {
            damage = weaponBehavior.damage;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.TakeDamage(damage, bleedDamage, true);
                Destroy(this.gameObject);
            }
        }

        if (col.tag == "Ground Grass")
        {
            Destroy(this.gameObject);
        }
    }
}
