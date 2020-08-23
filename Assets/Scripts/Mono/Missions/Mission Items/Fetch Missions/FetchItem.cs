using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchItem : MonoBehaviour
{

    MissionLog missionLog;

    int numOfTriggers = 1;

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
        if(col.tag == "Player")
        {
            numOfTriggers -= 1;

            if(numOfTriggers > -1)
            {
                if(missionLog.mission[missionLog.currentMission] is FetchMission)
                {
                    FetchMission fetchMission = missionLog.mission[missionLog.currentMission] as FetchMission;
                    fetchMission.ItemPickedUp();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
