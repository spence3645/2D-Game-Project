using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBehavior : MonoBehaviour
{
    public string buffName;
    public string armorModel;
    public string armorName;

    public int defense;
    public int rarity;

    public float buff;

    public GameObject player;

    public ParticleSystem armorRarity;

    public CharacterController characterController;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
        armorRarity = this.transform.Find("Rarity").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseColor()
    {
        if (rarity < 0)
        {
            RarityColor(Color.red);
        }
        else if (rarity == 0)
        {
            RarityColor(Color.white);
        }
        else if (rarity == 1)
        {
            RarityColor(Color.green);
            defense += 1;
        }
        else if (rarity == 2)
        {
            RarityColor(Color.blue);
            defense += 2;
        }
        else if (rarity >= 3 && rarity < 10)
        {
            RarityColor(Color.magenta);
            defense += 3;
        }
        else if (rarity >= 10 && rarity <= 20)
        {
            RarityColor(new Color(1f, 0.35f, 0f));
        }
        else if(rarity >= 30)
        {
            RarityColor(Color.yellow);
        }
    }

    public void NameArmor()
    {
        armorName += " " + armorModel;
    }

    public virtual void CreateArmor(float chance)
    {

    }

    public virtual void ArmorStats()
    {

    }

    public void RarityColor(Color color)
    {
        ParticleSystem.MainModule module = armorRarity.main;
        module.startColor = color;
    }
}
