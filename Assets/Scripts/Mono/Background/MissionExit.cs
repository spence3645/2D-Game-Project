using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionExit : MonoBehaviour
{

    void Start()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Hub");
        }
    }
}
