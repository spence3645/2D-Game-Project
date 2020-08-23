using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject weaponInSlot;
    GameObject deselectWeapon;
    GameObject deselectArmor;
    GameObject weaponStats;

    EquippedWeapon equippedWeaponScript;
    EquippedArmor equippedArmorScript;

    // Start is called before the first frame update
    void Start()
    {
        equippedWeaponScript = GameObject.Find("Weapon Slot").GetComponent<EquippedWeapon>();
        equippedArmorScript = GameObject.Find("Armor Slots").GetComponent<EquippedArmor>();
        weaponStats = GameObject.Find("Gun Stats Inventory");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.GetComponent<Image>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        transform.GetComponent<Image>().raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (weaponInSlot && weaponInSlot.GetComponent<WeaponBehavior>())
        {
            WeaponBehavior weaponBehavior = weaponInSlot.GetComponent<WeaponBehavior>();

            weaponStats.GetComponent<Canvas>().enabled = true;
            weaponStats.GetComponent<WeaponStatsInventory>().GetGunStat(weaponBehavior);
            Vector3 pointerPosition = eventData.position;
            //pointerPosition.y += 120;
            weaponStats.transform.position = pointerPosition;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (weaponInSlot)
        {
            weaponStats.GetComponent<Canvas>().enabled = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject() && eventData.pointerDrag.GetComponent<EquippedWeaponSlot>() && !weaponInSlot)
        {
            if(eventData.pointerDrag.GetComponent<EquippedWeaponSlot>().equippedWeapon != null)
            {
                DeselectWeapon(eventData);
            }
        }
        else if (EventSystem.current.IsPointerOverGameObject() && eventData.pointerDrag.GetComponent<EquippedWeaponSlot>() && weaponInSlot)
        {
            if (eventData.pointerDrag.GetComponent<EquippedWeaponSlot>().equippedWeapon != null)
            {
                SwapWeapon(eventData);
            }
        }
        else if (EventSystem.current.IsPointerOverGameObject() && eventData.pointerDrag.GetComponent<EquippedArmorSlot>() && !weaponInSlot)
        {
            if (eventData.pointerDrag.GetComponent<EquippedHelmetSlot>())
            {
                deselectArmor = eventData.pointerDrag.GetComponent<EquippedHelmetSlot>().equippedArmor;
                DeselectArmor(eventData, deselectArmor, "Helmet");
            }
            else if (eventData.pointerDrag.GetComponent<EquippedChestSlot>())
            {
                deselectArmor = eventData.pointerDrag.GetComponent<EquippedChestSlot>().equippedArmor;
                DeselectArmor(eventData, deselectArmor, "Chest");
            }
            else if (eventData.pointerDrag.GetComponent<EquippedLegSlot>())
            {
                deselectArmor = eventData.pointerDrag.GetComponent<EquippedLegSlot>().equippedArmor;
                DeselectArmor(eventData, deselectArmor, "Leg");
            }
        }
        else if (EventSystem.current.IsPointerOverGameObject() && eventData.pointerDrag.GetComponent<EquippedArmorSlot>() && weaponInSlot)
        {
            if (eventData.pointerDrag.GetComponent<EquippedHelmetSlot>() && this.weaponInSlot.gameObject.name.Contains("helmet"))
            {
                deselectArmor = eventData.pointerDrag.GetComponent<EquippedHelmetSlot>().equippedArmor;
                SwapArmor(eventData, deselectArmor, "Helmet");
            }
            else if (eventData.pointerDrag.GetComponent<EquippedChestSlot>() && this.weaponInSlot.gameObject.name.Contains("chest"))
            {
                deselectArmor = eventData.pointerDrag.GetComponent<EquippedChestSlot>().equippedArmor;
                SwapArmor(eventData, deselectArmor, "Chest");
            }
            else if (eventData.pointerDrag.GetComponent<EquippedLegSlot>() && this.weaponInSlot.gameObject.name.Contains("legs"))
            {
                deselectArmor = eventData.pointerDrag.GetComponent<EquippedLegSlot>().equippedArmor;
                SwapArmor(eventData, deselectArmor, "Leg");
            }
        }
        else if(EventSystem.current.IsPointerOverGameObject() && eventData.pointerDrag.GetComponent<InventorySlot>() && weaponInSlot)
        {
            if(eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot != null)
            {
                OrganizeTwoWeapons(eventData);
            }
        }
        else if(EventSystem.current.IsPointerOverGameObject() && eventData.pointerDrag.GetComponent<InventorySlot>() && !weaponInSlot)
        {
            if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot != null)
            {
                OrganizeOneWeapon(eventData);
            }
        }
    }

    void DeselectWeapon(PointerEventData eventData)
    {
        deselectWeapon = eventData.pointerDrag.GetComponent<EquippedWeaponSlot>().equippedWeapon;
        weaponInSlot = deselectWeapon;

        this.transform.Find("RarityColor").GetComponent<Image>().color = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = new Color(255, 255, 255, 0.45f);

        this.GetComponent<Image>().sprite = deselectWeapon.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 20);

        eventData.pointerDrag.GetComponent<Image>().sprite = null;
        eventData.pointerDrag.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        eventData.pointerDrag.GetComponent<EquippedWeaponSlot>().equippedWeapon = null;

        equippedWeaponScript.UnequipWeapon();
    }

    void SwapWeapon(PointerEventData eventData)
    {
        GameObject inventoryWeaponInfo = weaponInSlot;
        Color equippedColor = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        weaponInSlot = eventData.pointerDrag.GetComponent<EquippedWeaponSlot>().equippedWeapon;

        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = this.transform.Find("RarityColor").GetComponent<Image>().color;
        this.transform.Find("RarityColor").GetComponent<Image>().color = equippedColor;

        this.GetComponent<Image>().sprite = weaponInSlot.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 20);

        eventData.pointerDrag.GetComponent<Image>().sprite = inventoryWeaponInfo.GetComponent<SpriteRenderer>().sprite;
        eventData.pointerDrag.GetComponent<EquippedWeaponSlot>().equippedWeapon = inventoryWeaponInfo;
        eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 20);

        equippedWeaponScript.SwapWeapon(inventoryWeaponInfo);
    }

    void DeselectArmor(PointerEventData eventData, GameObject deselectArmor, string armorType)
    {
        weaponInSlot = deselectArmor;

        this.transform.Find("RarityColor").GetComponent<Image>().color = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = new Color(255, 255, 255, 0.45f);

        this.GetComponent<Image>().sprite = this.deselectArmor.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);

        eventData.pointerDrag.GetComponent<Image>().sprite = null;
        eventData.pointerDrag.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        eventData.pointerDrag.GetComponent<EquippedArmorSlot>().equippedArmor = null;

        equippedArmorScript.UnequipArmor(this.deselectArmor, armorType);
    }

    void SwapArmor(PointerEventData eventData, GameObject deselectArmor, string armorType)
    {
        GameObject inventoryLootInfo = weaponInSlot;
        Color equippedColor = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        weaponInSlot = deselectArmor;

        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = this.transform.Find("RarityColor").GetComponent<Image>().color;
        this.transform.Find("RarityColor").GetComponent<Image>().color = equippedColor;

        this.GetComponent<Image>().sprite = weaponInSlot.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);

        eventData.pointerDrag.GetComponent<Image>().sprite = inventoryLootInfo.GetComponent<SpriteRenderer>().sprite;
        eventData.pointerDrag.GetComponent<EquippedArmorSlot>().equippedArmor = inventoryLootInfo;
        eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);

        equippedArmorScript.SwapArmor(inventoryLootInfo, this.deselectArmor, armorType);
    }

    void OrganizeTwoWeapons(PointerEventData eventData)
    {
        GameObject inventoryWeaponInfo = weaponInSlot;
        Color equippedColor = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        weaponInSlot = eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot;

        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = this.transform.Find("RarityColor").GetComponent<Image>().color;
        this.transform.Find("RarityColor").GetComponent<Image>().color = equippedColor;

        this.GetComponent<Image>().sprite = weaponInSlot.GetComponent<SpriteRenderer>().sprite;
        if(weaponInSlot.tag == "Armor")
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);
        }
        else
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 20);
        }

        eventData.pointerDrag.GetComponent<Image>().sprite = inventoryWeaponInfo.GetComponent<SpriteRenderer>().sprite;
        eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot = inventoryWeaponInfo;
        if(inventoryWeaponInfo.tag == "Armor")
        {
            eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);
        }
        else
        {
            eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 20);
        }
    }

    void OrganizeOneWeapon(PointerEventData eventData)
    {
        deselectWeapon = eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot;
        weaponInSlot = deselectWeapon;

        this.transform.Find("RarityColor").GetComponent<Image>().color = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = new Color(255, 255, 255, 0.45f);

        this.GetComponent<Image>().sprite = deselectWeapon.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        if(deselectWeapon.tag == "Armor")
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);
        }
        else
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 20);
        }

        eventData.pointerDrag.GetComponent<Image>().sprite = null;
        eventData.pointerDrag.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot = null;
    }
}
