using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditHealth : EnemyHealth
{
    void Awake()
    {
        ID = 0;
        emitAmount = 10;
        health = 50;
        xp_amount = 5;
        starterHealth = health;

        blood_particle = this.GetComponentInChildren<ParticleSystem>();
    }
}
