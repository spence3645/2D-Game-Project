using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStats : MonoBehaviour
{
    bool started;

    WeaponBehavior weaponBehavior;

    public Text damageStat;
    public Text fireRateStat;
    public Text accuracyStat;
    public Text reloadSpeedStat;
    public Text magazineSizeStat;
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

            weaponBehavior = this.GetComponentInParent<WeaponBehavior>();
            backgroundColor = this.GetComponentInChildren<Image>();

            damageStat.text = weaponBehavior.damage.ToString();
            fireRateStat.text = CalculateFireRate().ToString();
            accuracyStat.text = (weaponBehavior.accuracy*100).ToString("0"); //Only two decimal points
            reloadSpeedStat.text = (weaponBehavior.reloadSpeed).ToString("0.0");
            magazineSizeStat.text = weaponBehavior.magazineSize.ToString();
            nameText.text = weaponBehavior.weaponName;


            SetBackgroundColor();
        }
    }

    int CalculateFireRate()
    {
        return (int)(60 / weaponBehavior.fireRate);
    }

    void SetBackgroundColor()
    {
        Color rarityColor = weaponBehavior.weaponRarity.main.startColor.color;
        rarityColor = new Color(rarityColor.r, rarityColor.g, rarityColor.b, 0.4f);
        backgroundColor.color = rarityColor;
    }
}
