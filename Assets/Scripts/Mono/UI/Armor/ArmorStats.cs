using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorStats : MonoBehaviour
{
    bool started;

    ArmorBehavior armorBehavior;

    public Text defense;
    public Text nameText;

    Image backgroundColor;

    // Start is called before the first frame update
    void Start()
    {
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Runs in update once because the weapon stats are decided in the Start method
        if (started)
        {
            started = false;

            armorBehavior = this.GetComponentInParent<ArmorBehavior>();
            backgroundColor = this.GetComponentInChildren<Image>();

            defense.text = armorBehavior.defense.ToString();
            nameText.text = armorBehavior.armorName;

            SetBackgroundColor();
        }
    }

    void SetBackgroundColor()
    {
        Color rarityColor = armorBehavior.armorRarity.main.startColor.color;
        rarityColor = new Color(rarityColor.r, rarityColor.g, rarityColor.b, 0.4f);
        backgroundColor.color = rarityColor;
    }
}
