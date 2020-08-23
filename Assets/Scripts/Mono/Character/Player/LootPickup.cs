using UnityEngine;

public class LootPickup : MonoBehaviour
{
    GameObject lootPickedUp;

    bool inRange;

    PocketInventory pocket;
    InventoryUI inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        pocket = GameObject.Find("Pocket Inventory").GetComponent<PocketInventory>();
        inventoryUI = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange && pocket.weapons.Count <= pocket.weapons.Capacity)
        {
            pocket.MoveGunToInventory(lootPickedUp);
            inventoryUI.FindOpenSlot(lootPickedUp);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Gun")
        {
            lootPickedUp = col.gameObject;
            col.GetComponentInChildren<Canvas>().enabled = true;
            ParticleSystem particle = col.GetComponentInChildren<ParticleSystem>();
            var emission = particle.emission;
            emission.enabled = false;
            inRange = true;
        }
        else if(col.tag == "Armor")
        {
            lootPickedUp = col.gameObject;
            col.GetComponentInChildren<Canvas>().enabled = true;
            ParticleSystem particle = col.GetComponentInChildren<ParticleSystem>();
            var emission = particle.emission;
            emission.enabled = false;
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Gun")
        {
            if (col.GetComponentInChildren<Canvas>() && col.GetComponentInChildren<ParticleSystem>())
            {
                col.GetComponentInChildren<Canvas>().enabled = false;
                ParticleSystem particle = col.GetComponentInChildren<ParticleSystem>();
                var emission = particle.emission;
                emission.enabled = true;
                inRange = false;
            }
        }
        else if (col.tag == "Armor")
        {
            if (col.GetComponentInChildren<Canvas>() && col.GetComponentInChildren<ParticleSystem>())
            {
                col.GetComponentInChildren<Canvas>().enabled = false;
                ParticleSystem particle = col.GetComponentInChildren<ParticleSystem>();
                var emission = particle.emission;
                emission.enabled = true;
                inRange = false;
            }
        }
    }
}
