using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquippedWeaponSlot : MonoBehaviour, IDropHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject equippedWeapon;
    public GameObject weaponStats;

    public EquippedWeapon equippedScript;

    // Start is called before the first frame update
    void Start()
    {
        equippedScript = GameObject.Find("Weapon Slot").GetComponent<EquippedWeapon>();
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
        if (equippedWeapon && equippedWeapon.GetComponent<WeaponBehavior>())
        {
            WeaponBehavior weaponBehavior = equippedWeapon.GetComponent<WeaponBehavior>();

            weaponStats.GetComponent<Canvas>().enabled = true;
            weaponStats.GetComponent<WeaponStatsInventory>().GetGunStat(weaponBehavior);
            Vector3 pointerPosition = eventData.position;
            //pointerPosition.y += 120;
            weaponStats.transform.position = pointerPosition;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (equippedWeapon)
        {
            weaponStats.GetComponent<Canvas>().enabled = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject() && !equippedWeapon)
        {
            if(eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.tag == "Gun")
            {
                EquipWeapon(eventData);
            }
        }
        else if(EventSystem.current.IsPointerOverGameObject() && equippedWeapon)
        {
            if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.tag == "Gun")
            {
                SwapWeapon(eventData);
            }
        }
    }

    void EquipWeapon(PointerEventData eventData)
    {
        equippedWeapon = eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot;

        this.transform.Find("RarityColor").GetComponent<Image>().color = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = new Color(255, 255, 255, 0.45f);

        this.GetComponent<Image>().sprite = equippedWeapon.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 20);

        eventData.pointerDrag.GetComponent<Image>().sprite = null;
        eventData.pointerDrag.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot = null;

        equippedScript.EquipWeapon(equippedWeapon);
    }

    void SwapWeapon(PointerEventData eventData)
    {
        GameObject equippedWeaponInfo = equippedWeapon;
        Color inventoryColor = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        equippedWeapon = eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot;

        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = this.transform.Find("RarityColor").GetComponent<Image>().color;
        this.transform.Find("RarityColor").GetComponent<Image>().color = inventoryColor;

        this.GetComponent<Image>().sprite = equippedWeapon.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 20);

        eventData.pointerDrag.GetComponent<Image>().sprite = equippedWeaponInfo.GetComponent<SpriteRenderer>().sprite;
        eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot = equippedWeaponInfo;
        eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 20);

        equippedScript.SwapWeapon(equippedWeapon);
    }
}
