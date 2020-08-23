using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float health;
    public float starterHealth;
    public int emitAmount;
    public int defense;

    public bool isBleeding;

    public Image healthBar;

    public ParticleSystem blood_particle;

    // Start is called before the first frame update
    void Start()
    {
        starterHealth = 200f;
        emitAmount = 10;
        health = starterHealth;
        blood_particle = this.transform.Find("Blood").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(health <= 0)
        //{
            //Die();
        //}
    }

    public void Heal(int healthAmount)
    {
        if(health < starterHealth)
        {
            if((starterHealth-health) < healthAmount)
            {
                health += (starterHealth-health);
                healthBar.fillAmount = (float)this.health / starterHealth;
            }
            else
            {
                health += healthAmount;
                healthBar.fillAmount = (float)this.health / starterHealth;
            }
        }
    }

    public void TakeDamage(float damage, int bleedDamage, bool hasBleed)
    {
        if(defense != 0)
        {
            float reducedDamage = damage - ((float)defense / 10);
            health -= reducedDamage;
        }
        else
        {
            float reducedDamage = damage;
            health -= reducedDamage;
        }
        healthBar.fillAmount = (float)health / starterHealth;
        if (blood_particle)
        {
            blood_particle.Emit(emitAmount);
        }

        if (hasBleed && !isBleeding)
        {
            isBleeding = true;
            StartCoroutine(Bleed(bleedDamage));
        }
    }

    public IEnumerator Bleed(int bleedDamage)
    {
        yield return new WaitForSeconds(1);
        health -= bleedDamage;
        healthBar.fillAmount = (float)health / starterHealth;
        if (blood_particle)
        {
            blood_particle.Emit(50);
        }
        yield return new WaitForSeconds(1);
        health -= bleedDamage;
        healthBar.fillAmount = (float)health / starterHealth;
        if (blood_particle)
        {
            blood_particle.Emit(50);
        }
        yield return new WaitForSeconds(1);
        health -= bleedDamage;
        healthBar.fillAmount = (float)health / starterHealth;
        if (blood_particle)
        {
            blood_particle.Emit(50);
        }

        isBleeding = false;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
