using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedArmor : MonoBehaviour
{
    public GameObject equippedHelmet;
    public GameObject equippedChest;
    public GameObject equippedLegs;

    public int helmetDefense;
    public int chestDefense;
    public int legsDefense;

    public float helmetBuff;
    public float chestBuff;
    public float legsBuff;

    GameObject player;

    PocketInventory pocketInventory;

    // Start is called before the first frame update
    void Start()
    {
        pocketInventory = GameObject.Find("Pocket Inventory").GetComponent<PocketInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    public void EquipHelmet(GameObject armorPiece)
    {
        for (int i = 0; i < pocketInventory.weapons.Count; i++)
        {
            if (pocketInventory.weapons[i].Equals(armorPiece))
            {
                pocketInventory.weapons[i].transform.parent = this.transform.Find("Helmet Slot");
                pocketInventory.weapons[i].SetActive(true);
            }
        }

        equippedHelmet = this.transform.Find("Helmet Slot").GetChild(0).gameObject;
        equippedHelmet.transform.position = this.transform.Find("Helmet Slot").position;
        equippedHelmet.transform.localScale = new Vector3(1, 1, 0);

        helmetDefense = equippedHelmet.GetComponent<ArmorBehavior>().defense;

        SetDefense(helmetDefense + chestDefense + legsDefense);
        //SetBuff(helmetBuff + chestBuff + legsBuff, armorPiece);
    }

    public void EquipChest(GameObject armorPiece)
    {
        for (int i = 0; i < pocketInventory.weapons.Count; i++)
        {
            if (pocketInventory.weapons[i].Equals(armorPiece))
            {
                pocketInventory.weapons[i].transform.parent = this.transform.Find("Chest Slot");
                pocketInventory.weapons[i].SetActive(true);
            }
        }

        equippedChest = this.transform.Find("Chest Slot").GetChild(0).gameObject;
        equippedChest.transform.position = this.transform.Find("Chest Slot").position;
        equippedChest.transform.localScale = new Vector3(1, 1, 0);

        chestDefense = equippedChest.GetComponent<ArmorBehavior>().defense;

        SetDefense(helmetDefense + chestDefense + legsDefense);
        //SetBuff(helmetBuff + chestBuff + legsBuff, armorPiece);
    }

    public void EquipLegs(GameObject armorPiece)
    {
        for (int i = 0; i < pocketInventory.weapons.Count; i++)
        {
            if (pocketInventory.weapons[i].Equals(armorPiece))
            {
                pocketInventory.weapons[i].transform.parent = this.transform.Find("Leg Slot");
                pocketInventory.weapons[i].SetActive(true);
            }
        }

        equippedLegs = this.transform.Find("Leg Slot").GetChild(0).gameObject;
        equippedLegs.transform.position = this.transform.Find("Leg Slot").position;
        equippedLegs.transform.localScale = new Vector3(1, 1, 0);

        legsDefense = equippedLegs.GetComponent<ArmorBehavior>().defense;

        SetDefense(helmetDefense + chestDefense + legsDefense);
        //SetBuff(helmetBuff + chestBuff + legsBuff, armorPiece);
    }

    public void SwapArmor(GameObject armorToSwap, GameObject armorPiece, string armorType)
    {
        for (int i = 0; i < pocketInventory.weapons.Count; i++)
        {
            if (pocketInventory.weapons[i].Equals(armorToSwap))
            {
                pocketInventory.weapons[i].transform.SetParent(this.transform.Find(armorType + " " + "Slot"));
                pocketInventory.weapons[i].transform.localPosition = Vector3.zero;
                pocketInventory.weapons[i].transform.localScale = new Vector3(1, 1, 0);
                pocketInventory.weapons[i].SetActive(true);
            }
        }

        pocketInventory.weapons.Add(armorPiece);
        armorPiece.transform.parent = pocketInventory.transform;
        armorPiece.SetActive(false);
        if (armorType == "Helmet")
        {
            equippedHelmet = this.transform.Find(armorType + " " + "Slot").GetChild(0).gameObject;
            helmetDefense = equippedHelmet.GetComponent<ArmorBehavior>().defense;
        }
        else if(armorType == "Chest")
        {
            equippedChest = this.transform.Find(armorType + " " + "Slot").GetChild(0).gameObject;
            chestDefense = equippedChest.GetComponent<ArmorBehavior>().defense;
        }
        else if(armorType == "Leg")
        {
            equippedLegs = this.transform.Find(armorType + " " + "Slot").GetChild(0).gameObject;
            legsDefense = equippedLegs.GetComponent<ArmorBehavior>().defense;
        }

        SetDefense(helmetDefense + chestDefense + legsDefense);
        //SetBuff(helmetBuff + chestBuff + legsBuff, armorPiece);
    }

    public void UnequipArmor(GameObject armorPiece, string armorType)
    {
        if(armorType == "Helmet")
        {
            armorPiece.transform.parent = pocketInventory.gameObject.transform;
            armorPiece.SetActive(false);
            armorPiece = null;

            helmetDefense = 0;
        }
        else if(armorType == "Chest")
        {
            armorPiece.transform.parent = pocketInventory.gameObject.transform;
            armorPiece.SetActive(false);
            armorPiece = null;

            chestDefense = 0;
        }
        else if(armorType == "Leg")
        {
            armorPiece.transform.parent = pocketInventory.gameObject.transform;
            armorPiece.SetActive(false);
            armorPiece = null;

            legsDefense = 0;
        }

        SetDefense(helmetDefense + chestDefense + legsDefense);
        //RemoveBuff(helmetBuff + chestBuff + legsBuff, armorPiece);
    }

    void SetDefense(int defense)
    {
        player.GetComponent<PlayerHealth>().defense = defense;
    }

    void SetBuff(float buff, string buffName)
    {
        if(buffName == "Jetpack")
        {

        }
    }

    void RemoveBuff(float buff, string buffName)
    {

    }
}
