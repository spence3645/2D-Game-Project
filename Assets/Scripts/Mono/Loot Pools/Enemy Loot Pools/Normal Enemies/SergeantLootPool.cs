using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SergeantLootPool : ParentLootPool
{
    DropAug aug_drop;
    DropAK ak_drop;
    DropVector vector_drop;
    DropPumpShotgun pump_drop;
    EnemyHealth enemyHealth;
    DropM1 m1_drop;
    DropScar scar_drop;
    DropAA12 aa12_drop;
    DropG36 g36_drop;
    DropMP5 mp5_drop;
    DropBarrett barrett_drop;
    DropDeagle deagle_drop;
    DropP90 p90_drop;
    DropXM8 xm8_drop;
    // Start is called before the first frame update
    void Start()
    {
        rarity_chance = 0.8f;

        ak_drop = this.GetComponentInChildren<DropAK>();
        vector_drop = this.GetComponentInChildren<DropVector>();
        pump_drop = this.GetComponentInChildren<DropPumpShotgun>();
        aug_drop = this.GetComponentInChildren<DropAug>();
        m1_drop = this.GetComponentInChildren<DropM1>();
        scar_drop = this.GetComponentInChildren<DropScar>();
        aa12_drop = this.GetComponentInChildren<DropAA12>();
        g36_drop = this.GetComponentInChildren<DropG36>();
        mp5_drop = this.GetComponentInChildren<DropMP5>();
        barrett_drop = this.GetComponentInChildren<DropBarrett>();
        deagle_drop = this.GetComponentInChildren<DropDeagle>();
        p90_drop = this.GetComponentInChildren<DropP90>();
        xm8_drop = this.GetComponentInChildren<DropXM8>();
        enemyHealth = this.gameObject.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LootRoll()
    {
        int roll = Random.Range(0, 13);
        if (roll == 0)
        {
            ak_drop.Drop(rarity_chance);
        }
        else if (roll == 1)
        {
            pump_drop.Drop(rarity_chance);
        }
        else if (roll == 2)
        {
            vector_drop.Drop(rarity_chance);
        }
        else if (roll == 3)
        {
            aug_drop.Drop(rarity_chance);
        }
        else if (roll == 4)
        {
            m1_drop.Drop(rarity_chance);
        }
        else if (roll == 5)
        {
            scar_drop.Drop(rarity_chance);
        }
        else if (roll == 6)
        {
            aa12_drop.Drop(rarity_chance);
        }
        else if (roll == 7)
        {
            g36_drop.Drop(rarity_chance);
        }
        else if (roll == 8)
        {
            mp5_drop.Drop(rarity_chance);
        }
        else if (roll == 9)
        {
            barrett_drop.Drop(rarity_chance);
        }
        else if (roll == 10)
        {
            deagle_drop.Drop(rarity_chance);
        }
        else if (roll == 11)
        {
            p90_drop.Drop(rarity_chance);
        }
        else if (roll == 12)
        {
            xm8_drop.Drop(rarity_chance);
        }
    }

    public override void DropLoot(EnemyHealth enemyHealth)
    {
        LootRoll();
        Destroy(enemyHealth.transform.parent.gameObject);
    }
}
