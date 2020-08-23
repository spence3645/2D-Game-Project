using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForetoldHelmet : ArmorBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void CreateArmor(float chance)
    {
        ArmorStats();

        rarity += 50;

        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            armorName = "Crusty";
            defense += 1;
            rarity += 1;
        }
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            armorName = "Refined";
            defense += 1;
            rarity += 1;
        }
        roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            armorName = "Factory New";
            defense += 1;
            rarity += 1;
        }

        ChooseColor();
        NameArmor();
    }

    public override void ArmorStats()
    {
        buffName = "Jetpack";
        armorModel = "Foretold Helmet";

        defense = 13;
        buff = 0.25f;
    }
}
