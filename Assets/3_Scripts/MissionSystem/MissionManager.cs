using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public int missionCompleted { get; private set; } = 0;
    private Dictionary<string, Mission> missionMap;

    [SerializeField] private GameObject missionEntryPoint;

    private void Awake()
    {
        missionMap = CreateMissionMap();
    }

    private void OnEnable()
    {
        if(!RemoteConfig.MISSIONS_ENABLED)
            gameObject.SetActive(false);

        EventManager.Instance.onStartMission += StartMission;
        EventManager.Instance.onAdvanceMission += AdvanceMission;
        EventManager.Instance.onFinishMission += FinishMission;

        EventManager.Instance.onMissionStepStateChange += MissionStepStateChange;

        missionEntryPoint.SetActive(RemoteConfig.MISSIONS_ENABLED);
    }

    private void OnDisable()
    {
        EventManager.Instance.onStartMission -= StartMission;
        EventManager.Instance.onAdvanceMission -= AdvanceMission;
        EventManager.Instance.onFinishMission -= FinishMission;

        EventManager.Instance.onMissionStepStateChange -= MissionStepStateChange;
    }

    private void Start()
    {
        foreach (Mission mission in missionMap.Values)
        {
            if (mission.missionState == MissionState.STARTED)
            {
                EventManager.Instance.StartMission(mission.missionInfo.Id);
                mission.InstantiateMissionStep(this.transform);
            }
            else if (mission.missionState == MissionState.MEET_REQUIREMENTS && CheckRequirements(mission))
            {
                ChangeMissionState(mission.missionInfo.Id, MissionState.STARTED);
                mission.InstantiateMissionStep(this.transform);
            }
        }
    }

    private void ChangeMissionState(string id, MissionState state)
    {
        Mission mission = GetMissionById(id);
        mission.missionState=state;
        EventManager.Instance.MissionStateChange(mission);
    }

    private bool CheckRequirements(Mission mission)
    {
        bool meetsRequirements = true;

        foreach(MissionInfoSO missionPrerequisite in mission.missionInfo.missionPrerequisite)
        {
            if(GetMissionById(missionPrerequisite.Id).missionState != MissionState.FINISHED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
    }

    private void StartMission(string id)
    {
        Mission mission = GetMissionById(id);
        mission.InstantiateMissionStep(this.transform);
        ChangeMissionState(mission.missionInfo.Id, MissionState.STARTED);
    }

    private void AdvanceMission(string id)
    {
        Mission mission = GetMissionById(id);
        mission.MoveToNextStep();

        if (mission.CurrentStepExists())
        {
            mission.InstantiateMissionStep(this.transform);
        }
        else
        {
            ChangeMissionState(mission.missionInfo.Id, MissionState.FINISHED);
            EventManager.Instance.FinishMission(mission.missionInfo.Id);
        }
    }

    private void FinishMission(string id)
    {
        Mission mission = GetMissionById(id);
        ChangeMissionState(mission.missionInfo.Id, MissionState.FINISHED);
    }

    private void MissionStepStateChange(string id, int stepIndex, MissionStepState missionStepState) 
    {
        Mission mission = GetMissionById(id);
        mission.StoreMissionStepState(missionStepState, stepIndex);
        ChangeMissionState(id, mission.missionState);
    }

    private Dictionary<string, Mission> CreateMissionMap()
    {
        MissionInfoSO[] allMissions = Resources.LoadAll<MissionInfoSO>("Missions");
        Dictionary<string, Mission> idToMissionMap = new Dictionary<string, Mission>();
        foreach (MissionInfoSO missionInfo in allMissions)
        {
            idToMissionMap.Add(missionInfo.Id, LoadMission(missionInfo));
        }
        return idToMissionMap;
    }

    public Mission GetMissionById(string id)
    {
        Mission mission = missionMap[id];

        if (mission == null)
        {
            Debug.LogError("ID not found in Mission Map:" + id);
        }
        return mission;
    }

    private void OnApplicationQuit()
    {
        foreach (Mission mission in missionMap.Values)
        {
            SaveMission(mission);
        }
    }

    private void OnDestroy()
    {
        foreach (Mission mission in missionMap.Values)
        {
            SaveMission(mission);
        }
    }

    private void SaveMission(Mission mission)
    {
        try
        {
            MissionData missionData = mission.GetMissionData();
            string serializedData = JsonUtility.ToJson(missionData);
            PlayerPrefs.SetString(mission.missionInfo.Id, serializedData);
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to save Mission with id " + mission.missionInfo.Id + ": " + e);
        }
    }

    private Mission LoadMission(MissionInfoSO missionInfoSO)
    {
        Mission mission = null;
        try
        {
            if (PlayerPrefs.HasKey(missionInfoSO.Id))
            {
                string serializedData = PlayerPrefs.GetString(missionInfoSO.Id);
                MissionData missionData = JsonUtility.FromJson<MissionData>(serializedData);
                mission = new Mission(missionInfoSO, missionData.state, missionData.missionStepIndex, missionData.missionStepStates);

                if (mission.missionState is MissionState.STARTED)
                {
                    Debug.Log("-_- called");
                    EventManager.Instance.MissionsLoad(mission.missionInfo.Id);
                }

                if(mission.missionState is MissionState.FINISHED)
                {
                    missionCompleted++;
                }
            }
            else
            {
                mission = new Mission(missionInfoSO);
            }
        } 
        catch(System.Exception e)
        {

        }
        return mission;
    }
}
