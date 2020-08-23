using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoBossPool : ParentLootPool
{

    DropAssassinDeagle assassin_deagle;

    // Start is called before the first frame update
    void Start()
    {
        rarity_chance = 0.5f;

        assassin_deagle = this.GetComponentInChildren<DropAssassinDeagle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LootRoll()
    {
        float roll = Random.Range(0f, 1f);
        if(roll <= 1f)
        {
            assassin_deagle.Drop(rarity_chance);
        }
    }

    public override void DropLoot(EnemyHealth enemyHealth)
    {
        LootRoll();
        Destroy(enemyHealth.transform.parent.gameObject);
    }
}
