using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedWeapon : MonoBehaviour
{
    public GameObject equippedWeapon;
    GameObject player;

    Text ammoText;

    PocketInventory pocketInventory;

    // Start is called before the first frame update
    void Start()
    {
        pocketInventory = GameObject.Find("Pocket Inventory").GetComponent<PocketInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (equippedWeapon)
        {
            ammoText.text = equippedWeapon.GetComponent<WeaponBehavior>().magazineTracker + "/" + equippedWeapon.GetComponent<WeaponBehavior>().magazineSize;
        }
    }

    public void EquipWeapon(GameObject weapon)
    {
        for(int i = 0; i < pocketInventory.weapons.Count; i++)
        {
            if (pocketInventory.weapons[i].Equals(weapon))
            {
                pocketInventory.weapons[i].transform.parent = this.transform;
                pocketInventory.weapons[i].SetActive(true);
                pocketInventory.weapons.Remove(pocketInventory.weapons[i]);
            }
        }

        equippedWeapon = this.gameObject.transform.GetChild(0).gameObject;
        equippedWeapon.transform.position = this.transform.position;
        equippedWeapon.transform.localScale = new Vector3(1, 1, 0);

        ammoText.text = equippedWeapon.GetComponent<WeaponBehavior>().magazineTracker + "/" + equippedWeapon.GetComponent<WeaponBehavior>().magazineSize;
    }

    public void SwapWeapon(GameObject weaponToSwap)
    {
        for (int i = 0; i < pocketInventory.weapons.Count; i++)
        {
            if (pocketInventory.weapons[i].Equals(weaponToSwap))
            {
                pocketInventory.weapons[i].transform.parent = this.transform;
                pocketInventory.weapons[i].SetActive(true);
                pocketInventory.weapons.Remove(pocketInventory.weapons[i]);
            }
        }

        pocketInventory.weapons.Add(equippedWeapon);
        equippedWeapon.transform.parent = pocketInventory.transform;
        equippedWeapon.SetActive(false);

        equippedWeapon = this.gameObject.transform.GetChild(0).gameObject;
        equippedWeapon.transform.position = this.transform.position;
        equippedWeapon.transform.localScale = new Vector3(1, 1, 0);

        ammoText.text = equippedWeapon.GetComponent<WeaponBehavior>().magazineTracker + "/" + equippedWeapon.GetComponent<WeaponBehavior>().magazineSize;
    }

    public void UnequipWeapon()
    {
        equippedWeapon.transform.parent = pocketInventory.gameObject.transform;
        pocketInventory.weapons.Add(equippedWeapon);
        equippedWeapon.SetActive(false);
        equippedWeapon = null;

        ammoText.text = "";
    }
}
