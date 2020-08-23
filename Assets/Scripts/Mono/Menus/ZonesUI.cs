using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZonesUI : MonoBehaviour
{

    GameObject zoneUI;

    public Button zone1Button;
    public Button zone2Button;
    public Button zone3Button;
    public Button zone4Button;
    public Button zone5Button;

    // Start is called before the first frame update
    void Start()
    {
        zoneUI = this.transform.Find("Zone Canvas").gameObject;
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>().level >= 21)
        {
            zone1Button.interactable = true;
            zone2Button.interactable = true;
            zone3Button.interactable = true;
            zone4Button.interactable = true;
            zone5Button.interactable = true;
        }
        else if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>().level >= 16)
        {
            zone1Button.interactable = true;
            zone2Button.interactable = true;
            zone3Button.interactable = true;
            zone4Button.interactable = true;
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>().level >= 11)
        {
            zone1Button.interactable = true;
            zone2Button.interactable = true;
            zone3Button.interactable = true;
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>().level >= 6)
        {
            zone1Button.interactable = true;
            zone2Button.interactable = true;
        }
        else
        {
            zone1Button.interactable = true;
        }
        zoneUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            zoneUI.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            zoneUI.SetActive(false);
        }
    }

    public void LoadZone1()
    {
        SceneManager.LoadScene("Zone1");
    }
    public void LoadZone2()
    {
        SceneManager.LoadScene("Zone2");
    }
    public void LoadZone3()
    {
        SceneManager.LoadScene("Zone3");
    }
    public void LoadZone4()
    {
        SceneManager.LoadScene("Zone4");
    }
    public void LoadZone5()
    {
        SceneManager.LoadScene("Zone5");
    }
}
