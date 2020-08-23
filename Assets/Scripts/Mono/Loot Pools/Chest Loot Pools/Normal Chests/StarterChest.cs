using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterChest : ParentChest
{

    public DropAug aug_drop;
    public DropAK ak_drop;
    public DropVector vector_drop;
    public DropPumpShotgun pump_drop;
    public DropM1 m1_drop;
    public DropScar scar_drop;
    public DropAA12 aa12_drop;
    public DropG36 g36_drop;
    public DropMP5 mp5_drop;
    public DropBarrett barrett_drop;
    public DropDeagle deagle_drop;
    public DropP90 p90_drop;
    public DropXM8 xm8_drop;
    public DropM16 m16_drop;

    public DropVoidRifle void_drop;

    // Start is called before the first frame update
    void Start()
    {
        rarity_chance = 1f;

        ak_drop = this.GetComponentInChildren<DropAK>();
        vector_drop = this.GetComponentInChildren<DropVector>();
        pump_drop = this.GetComponentInChildren<DropPumpShotgun>();
        aug_drop = this.GetComponentInChildren<DropAug>();
        m1_drop = this.GetComponentInChildren<DropM1>();
        aa12_drop = this.GetComponentInChildren<DropAA12>();
        scar_drop = this.GetComponentInChildren<DropScar>();
        g36_drop = this.GetComponentInChildren<DropG36>();
        mp5_drop = this.GetComponentInChildren<DropMP5>();
        barrett_drop = this.GetComponentInChildren<DropBarrett>();
        deagle_drop = this.GetComponentInChildren<DropDeagle>();
        p90_drop = this.GetComponentInChildren<DropP90>();
        xm8_drop = this.GetComponentInChildren<DropXM8>();
        m16_drop = this.GetComponentInChildren<DropM16>();

        void_drop = this.GetComponentInChildren<DropVoidRifle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Open()
    {
        int roll = Random.Range(14, 15);
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
        else if(roll == 4)
        {
            m1_drop.Drop(rarity_chance);
        }
        else if(roll == 5)
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
        else if(roll == 13)
        {
            m16_drop.Drop(rarity_chance);
        }
        else if(roll == 14)
        {
            void_drop.Drop(rarity_chance);
        }
    }
}
