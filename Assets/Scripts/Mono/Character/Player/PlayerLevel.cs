using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{

    public int level;
    public int xp_needed;
    public int current_xp;

    public GameObject xp_popup;

    public Image xp_bar;

    public Text level_text;

    // Start is called before the first frame update
    void Start()
    {
        level = 25;
        xp_needed = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Leveling();
    }

    void Leveling()
    {
        xp_bar.fillAmount = (float)current_xp / xp_needed;

        if(current_xp >= xp_needed)
        {
            xp_needed += 20;
            current_xp = 0;
            xp_bar.fillAmount = 0f;
            level += 1;
            level_text.text = level.ToString();
        }
    }

    public void SetXP(int xp)
    {
        ShowXP(xp);
        current_xp += xp;
    }

    void ShowXP(int xp)
    {
        GameObject popup = Instantiate(xp_popup, GameObject.Find("ProgressUI").transform);
        popup.transform.position = GameObject.FindGameObjectWithTag("Player").transform.Find("UI Position").position;
        if (popup.GetComponent<PopupScript>())
        {
            popup.GetComponent<PopupScript>().SetText("+" + xp.ToString() + " xp");
        }
    }
}
