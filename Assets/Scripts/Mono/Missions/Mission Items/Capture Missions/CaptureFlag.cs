using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureFlag : MonoBehaviour
{

    MissionLog missionLog;

    float captureAmount;

    bool capturing;

    public Image captureLength;

    // Start is called before the first frame update
    void Start()
    {
        missionLog = GameObject.Find("Character").GetComponent<MissionLog>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && !capturing && missionLog.mission[missionLog.currentMission] is CaptureMission)
        {
            CaptureMission captureMission = missionLog.mission[missionLog.currentMission] as CaptureMission;
            capturing = true;
            StartCoroutine(StartCapture(captureMission));
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            captureAmount = 0;
        }
    }

    IEnumerator StartCapture(CaptureMission capture)
    {
        captureAmount += 1;
        yield return new WaitForSeconds(0.1f);
        captureLength.fillAmount = captureAmount / 10;
        if (captureAmount == 10)
        {
            capture.FlagCaptured();
            Destroy(this.transform.parent.gameObject);
        }
        capturing = false;
    }
}
