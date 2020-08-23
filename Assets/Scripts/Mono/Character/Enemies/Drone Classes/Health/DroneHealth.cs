using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHealth : EnemyHealth
{
    void Awake()
    {
        ID = 1;
        health = 50;
        xp_amount = 10;
        starterHealth = health;
    }
}
