using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponBehavior : MonoBehaviour
{
    public int numOfProjectiles;
    public float damage;
    public float fireRate;
    public float nextFire = 0; //used with fire rate
    public float projectileSpeed;
    public float accuracy;
    public float damageDropoff;
    public float magazineSize;
    public float magazineTracker;
    public float reloadTime;
    public float reloadSpeed;

    public GameObject player;
    public GameObject bulletType;
    public GameObject gunBarrel;
    public GameObject magazine;

    public Vector2 aimDirection;
    public Vector3 mousePosition;

    public AIController aiController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    public virtual void CheckFire()
    {
        if (Time.time > nextFire && magazineTracker != 0 && Time.time > reloadTime)
        {
            magazineTracker--;
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    public virtual void CheckReload()
    {
        if (magazineTracker <= 0)
        {
            reloadTime = Time.time + reloadSpeed;
            Reload();
        }
    }

    public virtual void Aiming(Vector3 playerLocation)
    {
        if (aiController.isFacingRight)
        {
            aimDirection = new Vector2(playerLocation.x - this.transform.position.x, playerLocation.y - this.transform.position.y).normalized;
            this.transform.right = aimDirection;
        }
        //Inverse gun aiming to make it look correct
        else
        {
            aimDirection = new Vector2(playerLocation.x - this.transform.position.x, playerLocation.y - this.transform.position.y).normalized;
            this.transform.right = -aimDirection;
        }
    }


    //Copies equipped weapon and changes velocity relative to normalized mouse vector and projectile speed
    //TODO Projectile isn't launching directly in line with the mouse cursor
    public virtual void Fire()
    {
        GameObject bulletClone = Instantiate(bulletType, gunBarrel.transform.position, Quaternion.identity);

        //Adds accuracy to send bullets off path
        float offsetRange = 1 - accuracy;
        float bulletOffsetX = Random.Range(-offsetRange, offsetRange);
        float bulletOffsetY = Random.Range(-offsetRange, offsetRange);
        bulletClone.transform.right = aimDirection + new Vector2(bulletOffsetX, bulletOffsetY);

        bulletClone.GetComponent<SpriteRenderer>().enabled = true;
        bulletClone.GetComponent<CircleCollider2D>().enabled = true;

        if (aiController.isFacingRight)
        {
            bulletClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletClone.transform.right.x * projectileSpeed, bulletClone.transform.right.y * projectileSpeed));
        }
        else
        {
            bulletClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bulletClone.transform.right.x * -projectileSpeed, -bulletClone.transform.right.y * -projectileSpeed));
        }
    }

    public void Reload()
    {
        if (magazine)
        {
            GameObject magazineCopy = Instantiate(magazine, this.transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(magazineCopy.GetComponent<PolygonCollider2D>(), aiController.gameObject.GetComponent<BoxCollider2D>());
        }

        magazineTracker = magazineSize;
    }
}
