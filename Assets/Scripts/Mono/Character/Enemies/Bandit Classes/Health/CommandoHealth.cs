using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoHealth : EnemyHealth
{
    void Awake()
    {
        ID = 3;
        emitAmount = 10;
        health = 750;
        xp_amount = 50;
        starterHealth = health;

        blood_particle = this.GetComponentInChildren<ParticleSystem>();
    }
}
