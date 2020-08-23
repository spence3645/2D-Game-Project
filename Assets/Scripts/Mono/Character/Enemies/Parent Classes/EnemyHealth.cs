using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IEnemy
{
    public int ID { get; set; }

    public int health;
    public int starterHealth;
    public int emitAmount;
    public int xp_amount;

    public bool isBleeding;

    public GameObject damagePopup;
    public GameObject healthDrop;

    public Canvas healthCanvas;

    public Image healthBar;

    public ParticleSystem blood_particle;

    public MissionLog missionLog;
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        missionLog = GameObject.Find("Character").GetComponent<MissionLog>();
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        if(this.transform.Find("UI Position"))
        {
            Vector2 healthPosition = this.transform.Find("UI Position").position;
            healthCanvas.transform.position = healthPosition;
        }
    }

    public void TakeDamage(int damage, int bleedDamage, bool hasBleed)
    {
        soundManager.PlaySound("Enemy Shot");

        ShowDamage(damage);
        health -= damage;
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
        ShowDamage(bleedDamage);
        health -= bleedDamage;
        healthBar.fillAmount = (float)health / starterHealth;
        if (blood_particle)
        {
            blood_particle.Emit(50);
        }
        yield return new WaitForSeconds(1);
        ShowDamage(bleedDamage);
        health -= bleedDamage;
        healthBar.fillAmount = (float)health / starterHealth;
        if (blood_particle)
        {
            blood_particle.Emit(50);
        }
        yield return new WaitForSeconds(1);
        ShowDamage(bleedDamage);
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>().SetXP(xp_amount);

        if (!missionLog.isCompleted)
        {
            if (missionLog.mission[missionLog.currentMission] is KillMission)
            {
                KillMission killMission = missionLog.mission[missionLog.currentMission] as KillMission;
                killMission.EnemyKilled(this);
            }
            else if (missionLog.mission[missionLog.currentMission] is FetchMission)
            {
                if (this.GetComponent<FetchItemDrop>())
                {
                    this.GetComponent<FetchItemDrop>().DropItem();
                }
            }
        }

        this.GetComponent<ParentLootPool>().DropLoot(this);

        float roll = Random.Range(0f, 1f);
        if (roll <= 0.8)
        {
            Instantiate(healthDrop, this.transform.position, Quaternion.identity, null);
        }
    }

    void ShowDamage(int damage)
    {
        GameObject popup = Instantiate(damagePopup, new Vector2(this.transform.position.x + Random.Range(-0.5f, 0.5f), this.transform.position.y + Random.Range(0.5f, 1f)), Quaternion.identity, GameObject.Find("World Canvas").transform);
        if (popup.GetComponent<PopupScript>())
        {
            popup.GetComponent<PopupScript>().SetText(damage.ToString());
        }
    }
}
