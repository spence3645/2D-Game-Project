using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SergeantHealth : EnemyHealth
{
    void Awake()
    {
        ID = 2;
        emitAmount = 10;
        health = 250;
        xp_amount = 25;
        starterHealth = health;

        blood_particle = this.GetComponentInChildren<ParticleSystem>();
    }
}
