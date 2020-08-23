using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquippedArmorSlot : MonoBehaviour
{
    public GameObject equippedArmor;
    public GameObject deselectArmor;

    public EquippedArmor equippedScript;

    // Start is called before the first frame update
    void Start()
    {
        equippedScript = GameObject.Find("Armor Slots").GetComponent<EquippedArmor>();
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

    public void OnDrop(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject() && !equippedArmor)
        {
            if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.tag == "Armor" && eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.name.Contains("helmet"))
            {
                EquipArmorPiece(eventData, "Helmet");
            }
            else if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.tag == "Armor" && eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.name.Contains("chest"))
            {
                EquipArmorPiece(eventData, "Chest");
            }
            else if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.tag == "Armor" && eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.name.Contains("legs"))
            {
                EquipArmorPiece(eventData, "Legs");
            }
        }

        else if(EventSystem.current.IsPointerOverGameObject() && equippedArmor)
        {
            if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.tag == "Armor" && eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.name.Contains("helmet"))
            {
                SwapArmorPiece(eventData, "Helmet");
            }
            else if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.tag == "Armor" && eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.name.Contains("chest"))
            {
                SwapArmorPiece(eventData, "Chest");
            }
            else if (eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.tag == "Armor" && eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot.name.Contains("legs"))
            {
                SwapArmorPiece(eventData, "Legs");
            }
        }
    }

    public virtual void EquipArmorPiece(PointerEventData eventData, string armorType)
    {
        
    }

    public virtual void SwapArmorPiece(PointerEventData eventData, string armorType)
    {

    }
}
