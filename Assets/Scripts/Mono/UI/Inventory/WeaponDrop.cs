using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponDrop : MonoBehaviour, IDropHandler
{
    PocketInventory pocketInventory;

    GameObject droppedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        pocketInventory = GameObject.Find("Pocket Inventory").GetComponent<PocketInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject() && eventData.pointerDrag.GetComponent<InventorySlot>())
        {
            if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot != null)
            {
                DropWeapon(eventData);
            }
        }
    }

    void DropWeapon(PointerEventData eventData)
    {
        droppedWeapon = eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot;

        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = new Color(255, 255, 255, 0.45f);
        eventData.pointerDrag.GetComponent<Image>().sprite = null;
        eventData.pointerDrag.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot = null;

        pocketInventory.DropWeapon(droppedWeapon);
    }
}
