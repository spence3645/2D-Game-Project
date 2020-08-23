using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorChest : ParentChest
{

    public DropForetoldArmor foretoldArmor;

    // Start is called before the first frame update
    void Start()
    {
        rarity_chance = 0.4f;

        foretoldArmor = this.GetComponentInChildren<DropForetoldArmor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Open()
    {
        foretoldArmor.Drop(rarity_chance);
    }
}
