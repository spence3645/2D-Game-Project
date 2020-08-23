using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    GameObject inventoryUIUnequipped;
    GameObject inventoryUI;

    PocketInventory pocketInventory;
    List<InventorySlot> slots = new List<InventorySlot>();

    public bool isInventoryOpen;

    // Start is called before the first frame update
    void Start()
    {
        inventoryUIUnequipped = GameObject.Find("Unequipped");
        inventoryUI = this.gameObject;

        inventoryUI.GetComponent<Canvas>().enabled = false;
        inventoryUI.GetComponent<GraphicRaycaster>().enabled = false;
        inventoryUI.transform.Find("Layout Canvas").GetComponent<GraphicRaycaster>().enabled = false;

        GetSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isInventoryOpen)
        {
            isInventoryOpen = true;
            inventoryUI.GetComponent<Canvas>().enabled = true;
            inventoryUI.GetComponent<GraphicRaycaster>().enabled = true;
            inventoryUI.transform.Find("Layout Canvas").GetComponent<GraphicRaycaster>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isInventoryOpen)
        {
            isInventoryOpen = false;
            inventoryUI.GetComponent<Canvas>().enabled = false;
            inventoryUI.GetComponent<GraphicRaycaster>().enabled = false;
            inventoryUI.transform.Find("Layout Canvas").GetComponent<GraphicRaycaster>().enabled = false;
        }

        if (isInventoryOpen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void GetSlots()
    {
        for (int i = 0; i < inventoryUIUnequipped.transform.childCount; i++)
        {
            slots.Add(inventoryUIUnequipped.transform.GetChild(i).GetChild(0).GetComponent<InventorySlot>());
        }
    }

    public void FindOpenSlot(GameObject loot)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].weaponInSlot)
            {
                if (slots[i].weaponInSlot.Equals(loot))
                {
                    break;
                }
            }
            else if (!slots[i].weaponInSlot)
            {
                slots[i].weaponInSlot = loot;
                slots[i].GetComponent<Image>().sprite = loot.GetComponent<SpriteRenderer>().sprite;
                slots[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);

                if (loot.GetComponent<WeaponBehavior>())
                {
                    Color lootRarity = loot.GetComponent<WeaponBehavior>().weaponRarity.main.startColor.color;
                    slots[i].transform.Find("RarityColor").GetComponent<Image>().color = new Color(lootRarity.r, lootRarity.g, lootRarity.b, 0.2f); //Lets the player know what rarity the weapon is in inventory
                    slots[i].GetComponent<RectTransform>().sizeDelta = new Vector2(35, 20);
                }
                else if (loot.GetComponent<ArmorBehavior>())
                {
                    Color lootRarity = loot.GetComponent<ArmorBehavior>().armorRarity.main.startColor.color;
                    slots[i].transform.Find("RarityColor").GetComponent<Image>().color = new Color(lootRarity.r, lootRarity.g, lootRarity.b, 0.2f); //Lets the player know what rarity the weapon is in inventory
                    slots[i].GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);
                }

                break;
            }

            continue;
        }
    }
}
