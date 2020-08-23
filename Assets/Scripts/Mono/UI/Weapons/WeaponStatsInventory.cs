using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStatsInventory : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetGunStat(WeaponBehavior weaponBehavior)
    {
        backgroundColor = this.GetComponentInChildren<Image>();

        damageStat.text = weaponBehavior.damage.ToString();
        fireRateStat.text = CalculateFireRate(weaponBehavior).ToString();
        accuracyStat.text = (weaponBehavior.accuracy * 100).ToString("0"); //Only two decimal points
        reloadSpeedStat.text = weaponBehavior.reloadSpeed.ToString("0.0");
        magazineSizeStat.text = weaponBehavior.magazineSize.ToString();
        nameText.text = weaponBehavior.weaponName;

        SetBackgroundColor(weaponBehavior);
    }

    int CalculateFireRate(WeaponBehavior weaponBehavior)
    {
        return (int)(60 / weaponBehavior.fireRate);
    }

    void SetBackgroundColor(WeaponBehavior weaponBehavior)
    {
        Color rarityColor = weaponBehavior.weaponRarity.main.startColor.color;
        rarityColor = new Color(rarityColor.r, rarityColor.g, rarityColor.b, 0.7f);
        backgroundColor.color = rarityColor;
    }
}
