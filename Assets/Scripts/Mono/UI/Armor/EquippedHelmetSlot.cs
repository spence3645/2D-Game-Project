using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquippedHelmetSlot : EquippedArmorSlot, IDropHandler, IDragHandler, IEndDragHandler
{
    public override void EquipArmorPiece(PointerEventData eventData, string armorType)
    {
        if(armorType == "Helmet")
        {
            equippedArmor = eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot;

            this.transform.Find("RarityColor").GetComponent<Image>().color = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
            eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = new Color(255, 255, 255, 0.45f);

            this.GetComponent<Image>().sprite = equippedArmor.GetComponent<SpriteRenderer>().sprite;
            this.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);

            eventData.pointerDrag.GetComponent<Image>().sprite = null;
            eventData.pointerDrag.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot = null;

            equippedScript.EquipHelmet(equippedArmor);
        }
    }

    public override void SwapArmorPiece(PointerEventData eventData, string armorType)
    {
        GameObject equippedArmorInfo = equippedArmor;
        Color inventoryColor = eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color;
        equippedArmor = eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot;

        eventData.pointerDrag.transform.Find("RarityColor").GetComponent<Image>().color = this.transform.Find("RarityColor").GetComponent<Image>().color;
        this.transform.Find("RarityColor").GetComponent<Image>().color = inventoryColor;

        this.GetComponent<Image>().sprite = equippedArmor.GetComponent<SpriteRenderer>().sprite;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);

        eventData.pointerDrag.GetComponent<Image>().sprite = equippedArmorInfo.GetComponent<SpriteRenderer>().sprite;
        eventData.pointerDrag.GetComponent<InventorySlot>().weaponInSlot = equippedArmorInfo;
        eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 25);

        equippedScript.SwapArmor(equippedArmor, equippedArmorInfo, "Helmet");
    }
}
