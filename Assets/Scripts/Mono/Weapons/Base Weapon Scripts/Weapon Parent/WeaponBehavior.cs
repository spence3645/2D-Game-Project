using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehavior : MonoBehaviour
{
    public string weaponModel;
    public string weaponName;

    public int numOfProjectiles;
    public int damage;
    public int rarity;

    public float fireRate;
    public float fireRateSecondary;
    public float projectileSpeed;
    public float accuracy;
    public float damageDropoff;
    public float nextFire = 0; //used with fire rate
    public float nextFireSecondary = 0;
    public float magazineSize;
    public float magazineTracker;
    public float reloadTime;
    public float reloadSpeed;

    public bool isReloading;
    public bool hasSecondary;

    //Used for naming system
    public bool hasGrip;
    public bool hasRapid;
    public bool hasHolo;
    public bool hasScope;
    public bool hasReflex;
    public bool hasExtended;
    public bool hasFatBarrel;
    public bool hasLongBarrel;
    public bool hasSilencer;
    public bool hasPrecisionStock;
    public bool hasShortStock;

    public GameObject player;
    public GameObject bulletType;
    public GameObject secondaryBullet;
    public GameObject muzzleFlash;
    public GameObject gunBarrel;
    public GameObject secondaryBarrel;
    public GameObject magazine;

    public ParticleSystem weaponRarity;

    public Image reloadCircle;

    public Vector2 aimDirection;
    public Vector3 mousePosition;

    public CharacterController characterController;
    public SoundManager soundManager;
    public InventoryUI inventoryUI; //No shooting while inventory open

    // Awake is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        inventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
        bulletType = this.transform.Find("Bullet").gameObject;
        weaponRarity = this.transform.Find("Weapon Rarity").GetComponent<ParticleSystem>();
        reloadCircle = GameObject.Find("ProgressUI").GetComponentInChildren<Image>();

        if (this.transform.Find("Secondary Bullet"))
        {
            secondaryBullet = this.transform.Find("Secondary Bullet").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(magazineTracker <= 0)
        {
            Reload();
        }
    }

    public void CheckReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && magazineTracker != magazineSize)
        {
            Reload();
        }

        else if(Time.time <= reloadTime)
        {
            reloadCircle.fillAmount = (reloadTime-Time.time) / reloadSpeed;
            reloadCircle.rectTransform.position = player.transform.Find("UI Position").position;
        }

        else if(isReloading && Time.time >= reloadTime)
        {
            isReloading = false;
            reloadCircle.enabled = false;
        }
    }

    public void CheckFire()
    {
        if (this.transform.parent)
        {
            if (Input.GetMouseButton(0) && this.transform.parent.name == "Weapon Slot" && Time.time > reloadTime)
            {
                if (Time.time > nextFire && magazineTracker > 0 && !inventoryUI.isInventoryOpen)
                {
                    nextFire = Time.time + fireRate;
                    StartCoroutine(MuzzleFlash());
                    Fire();
                }
            }
            else if(Input.GetMouseButton(1) && this.transform.parent.name == "Weapon Slot" && hasSecondary)
            {
                if(Time.time > nextFireSecondary && !inventoryUI.isInventoryOpen)
                {
                    nextFireSecondary = Time.time + fireRateSecondary;
                    SecondaryFire();
                }
            }
        }
    }

    public void CheckAim()
    {
        if (this.transform.parent && this.transform.parent.name == "Weapon Slot")
        {
            Aiming();
        }
    }

    public void Aiming()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = new Vector2(mousePosition.x - this.transform.position.x, mousePosition.y - this.transform.position.y).normalized;

        if (characterController.isFacingRight)
        {
            this.transform.right = aimDirection;
        }
        //Inverse gun aiming to make it look correct
        else
        {
            this.transform.right = -aimDirection;
        }
    }

    public IEnumerator MuzzleFlash()
    {
        if (muzzleFlash)
        {
            GameObject flash = Instantiate(muzzleFlash, gunBarrel.transform);
            float randomScale = Random.Range(0.8f, 1f);
            flash.transform.localScale = new Vector2(randomScale, randomScale);
            yield return new WaitForSeconds(0.04f);
            Destroy(flash.gameObject);
        }
    }

    //Copies equipped weapon and changes velocity relative to normalized mouse vector and projectile speed
    //TODO Projectile isn't launching directly in line with the mouse cursor
    public virtual void Fire()
    {
        soundManager.PlaySound("Shooting");

        magazineTracker--;

        GameObject bulletClone = Instantiate(bulletType, gunBarrel.transform.position, Quaternion.identity);

        //Adds accuracy to send bullets off path
        float offsetRange = 1 - accuracy;
        float bulletOffsetX = Random.Range(-offsetRange, offsetRange);
        float bulletOffsetY = Random.Range(-offsetRange, offsetRange);
        bulletClone.transform.right = aimDirection + new Vector2(bulletOffsetX, bulletOffsetY);

        bulletClone.GetComponent<SpriteRenderer>().enabled = true;
        bulletClone.GetComponent<CircleCollider2D>().enabled = true;

        if (characterController.isFacingRight)
        {
            bulletClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletClone.transform.right.x * projectileSpeed, bulletClone.transform.right.y * projectileSpeed));
        }
        else
        {
            bulletClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bulletClone.transform.right.x * -projectileSpeed, -bulletClone.transform.right.y * -projectileSpeed));
        }
    }

    public virtual void SecondaryFire()
    {

    }

    public void Reload()
    {
        isReloading = true;

        reloadCircle.enabled = true;
        reloadCircle.rectTransform.position = player.transform.Find("UI Position").position;
        reloadTime = Time.time + reloadSpeed;
        reloadCircle.fillAmount = 1;

        if (magazine)
        {
            GameObject magazineCopy = Instantiate(magazine, this.transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(magazineCopy.GetComponent<PolygonCollider2D>(), player.GetComponent<BoxCollider2D>());
        }

        magazineTracker = magazineSize;
    }

    public void ChooseColor()
    {
        if(rarity < 0)
        {
            RarityColor(Color.red);
        }
        else if (rarity == 0)
        {
            RarityColor(Color.white);
        }
        else if (rarity == 1)
        {
            RarityColor(Color.green);
            damage += 1;
        }
        else if (rarity == 2)
        {
            RarityColor(Color.blue);
            damage += 2;
        }
        else if (rarity >= 3 && rarity < 10)
        {
            RarityColor(Color.magenta);
            damage += 3;
        }
        else if (rarity >= 10 && rarity <= 20)
        {
            RarityColor(new Color(1f, 0.35f, 0f));
        }
        else if (rarity >= 30)
        {
            RarityColor(Color.yellow);
        }
    }

    public void NameWeapon()
    {
        if (hasGrip)
        {
            weaponName += "Gripped" + " ";
        }
        else if (hasRapid)
        {
            weaponName += "Rapid" + " ";
        }

        if (hasScope)
        {
            weaponName += "Scoped" + " ";
        }

        else if (hasReflex)
        {
            weaponName += "Reflex" + " ";
        }

        if (hasExtended)
        {
            weaponName += "Extended" + " ";
        }

        if (hasSilencer)
        {
            weaponName += "Silenced" + " ";
        }
        else if (hasFatBarrel)
        {
            weaponName += "Fatty" + " ";
        }
        else if (hasLongBarrel)
        {
            weaponName += "Long" + " ";
        }

        if (hasPrecisionStock)
        {
            weaponName += "Precision" + " ";
        }
        else if (hasShortStock)
        {
            weaponName += "Short" + " ";
        }

        weaponName += weaponModel;
    }

    public virtual void CreateGun(float chance)
    {

    }

    public virtual void GunStats()
    {

    }

    public void RarityColor(Color color)
    {
        ParticleSystem.MainModule module = weaponRarity.main;
        module.startColor = color;
    }

    public virtual void WhiteStats()
    {
        
    }

    public virtual void GreenStats()
    {
        
    }

    public virtual void BlueStats()
    {
        
    }
}
