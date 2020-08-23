using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidChest : ArmorBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        CreateArmor(0.9f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void CreateArmor(float chance)
    {
        ArmorStats();

        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            rarity += 1;
        }
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            rarity += 1;
        }
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            rarity += 1;
        }

        ChooseColor();
    }

    public override void ArmorStats()
    {
        defense = 2;
    }
}
