using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{

    int healAmount = 20;
    int numOfTriggers = 1;

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            numOfTriggers -= 1;

            if (numOfTriggers > -1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Heal(healAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
