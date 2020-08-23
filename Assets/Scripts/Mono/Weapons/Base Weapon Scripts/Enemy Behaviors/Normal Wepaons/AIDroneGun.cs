using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDroneGun : AIWeaponBehavior
{
    // Start is called before the first frame update
    void Awake()
    {
        fireRate = Random.Range(2f, 2.5f);
        accuracy = Random.Range(0.6f, 0.8f);
        damage = 4;
        projectileSpeed = 750f;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunBarrel = this.transform.Find("Barrel").gameObject;
        bulletType = this.transform.Find("Bullet").gameObject;
        aiController = this.transform.GetComponentInParent<AIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiController.isActive)
        {
            Aiming(player.transform.position);
            CheckFire();
        }
    }

    public override void CheckFire()
    {
        if (Time.time > nextFire && Time.time > reloadTime)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    public override void Aiming(Vector3 playerLocation)
    {
        aimDirection = new Vector2(playerLocation.x - this.transform.position.x, playerLocation.y - this.transform.position.y).normalized;
        this.transform.right = -aimDirection;
    }
}
