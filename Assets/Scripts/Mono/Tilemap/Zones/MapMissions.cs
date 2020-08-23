using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMissions : MonoBehaviour
{
    // Start is called before the first frame update
    void OnLevelWasLoaded()
    {
        GameObject.Find("Character").GetComponent<MissionLog>().mission.Add(new FetchMission("Pick Up 10 Apples", false, 0, 1));
        GameObject.Find("Character").GetComponent<MissionLog>().mission.Add(new CaptureMission("Capture Point", false, 0, 1));
        GameObject.Find("Character").GetComponent<MissionLog>().mission.Add(new KillMission(0, "Kill 10 Bandits", false, 0, 1));
        GameObject.Find("Character").GetComponent<MissionLog>().mission.Add(new KillMission(1, "Kill 5 Drone", false, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
