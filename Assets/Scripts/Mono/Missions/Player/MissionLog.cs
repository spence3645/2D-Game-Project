using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionLog : MonoBehaviour
{

    public List<Mission> mission = new List<Mission>();
    public List<Text> displayMissions = new List<Text>();

    public int currentMission = 0;

    public bool isCompleted;
    public bool flagSpawned;

    public GameObject missionPanel;
    public GameObject rappelRope;
    public GameObject chest;
    public GameObject capture_point;

    public Sprite incomplete;
    public Sprite complete;

    // Start is called before the first frame update
    void Start()
    {
        missionPanel = GameObject.Find("Mission Panel");

        ResetMissions();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Hub")
        {
            isCompleted = false;

            mission.Clear();
            displayMissions.Clear();
            currentMission = 0;

            ResetMissions();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMission < mission.Count)
        {
            CheckMission();
        }
        else if(currentMission >= mission.Count && mission.Count > 0 && !isCompleted)
        {
            CompleteMission();
        }
        
        if(!flagSpawned && mission.Count != 0 && mission[currentMission] is CaptureMission)
        {
            flagSpawned = true;

            //-2 so chest and rappel not included, create the flag
            Vector3 randomPos = GameObject.Find("Grid").GetComponent<ZoneGenerator>().grassTileLocations[Random.Range(0, GameObject.Find("Grid").GetComponent<ZoneGenerator>().grassTileLocations.Count-2)];
            Vector3 spawn_flag = new Vector3(randomPos.x, randomPos.y + 2.8f, 0);
            Instantiate(capture_point, spawn_flag/2, Quaternion.identity, null);
        }
    }

    void CheckMission()
    {
        if (mission.Count != 0 && !mission[currentMission].isDone)
        {
            displayMissions[currentMission].text = mission[currentMission].description + ": " + mission[currentMission].currentAmount + "/" + mission[currentMission].requiredAmount;
        }

        else if (mission.Count != 0 && mission[currentMission].isDone && currentMission < mission.Count)
        {
            displayMissions[currentMission].text = mission[currentMission].description + ": " + mission[currentMission].currentAmount + "/" + mission[currentMission].requiredAmount;
            displayMissions[currentMission].GetComponentInChildren<Image>().sprite = complete;
            currentMission++;
        }
    }
    
    void CompleteMission()
    {
        isCompleted = true;
        rappelRope.SetActive(true);
        chest.SetActive(true);
    }

    void ResetMissions()
    {
        foreach (Text text in missionPanel.GetComponentsInChildren<Text>())
        {
            displayMissions.Add(text);
            text.text = "Challenge - Unknown";
            text.GetComponentInChildren<Image>().sprite = incomplete;
        }
    }
}
