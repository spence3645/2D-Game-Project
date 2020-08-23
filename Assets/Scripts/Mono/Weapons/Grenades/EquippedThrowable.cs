using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedThrowable : MonoBehaviour
{

    GameObject equippedWeapon;
    GameObject projectileParent;
    ThrowableBehavior throwBehavior;

    Transform handPos;

    float fireRate;
    float nextFire = 0; //used with fire rate
    float projectileSpeed;
    int numOfProjectiles;

    Vector2 normalizedPos;

    // Start is called before the first frame update
    void Start()
    {
        handPos = this.gameObject.transform;
        equippedWeapon = this.gameObject.transform.GetChild(0).gameObject;
        projectileParent = GameObject.Find("Projectiles");
    }

    // Update is called once per frame
    void Update()
    {
        GetWeaponBehavior();

        equippedWeapon.transform.position = handPos.position; 

        if (Input.GetMouseButtonDown(1) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector2 mousePos = Input.mousePosition;
            Vector2 tempProjectilePoint = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
            normalizedPos = tempProjectilePoint.normalized;
            Debug.Log(normalizedPos);
            Fire();
        }
    }

    //Grabs the weapon behavior on the equipped weapon
    void GetWeaponBehavior()
    {
        throwBehavior = equippedWeapon.GetComponent<ThrowableBehavior>();
        fireRate = throwBehavior.fireRate;
        numOfProjectiles = throwBehavior.numOfProjectiles;
        projectileSpeed = throwBehavior.projectileSpeed;
    }

    //Copies equipped weapon and changes velocity relative to normalized mouse vector and projectile speed
    //TODO Projectile isn't launching directly in line with the mouse cursor
    void Fire()
    {
        GameObject weaponClone = Instantiate(equippedWeapon, handPos.transform.position, Quaternion.identity, projectileParent.transform);

        weaponClone.GetComponent<SpriteRenderer>().enabled = true;
        weaponClone.GetComponent<CircleCollider2D>().enabled = true;
        weaponClone.GetComponent<Rigidbody2D>().velocity = new Vector2(normalizedPos.x*projectileSpeed, normalizedPos.y*projectileSpeed);
    }
}
