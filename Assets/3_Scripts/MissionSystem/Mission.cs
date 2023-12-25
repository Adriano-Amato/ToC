using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public MissionInfoSO missionInfo;
    public MissionState missionState;
    public int currentMissionStepIndex { get; private set;}
    private MissionStepState[] missionStepStates;

    public Mission(MissionInfoSO missionSO)
    {
        this.missionInfo = missionSO;
        this.missionState = MissionState.MEET_REQUIREMENTS;
        this.currentMissionStepIndex = 0;
        this.missionStepStates = new MissionStepState[missionInfo.missionStepPrefabs.Length];

        for(int i = 0;i<missionStepStates.Length;i++)
        {
            missionStepStates[i] = new MissionStepState();
        }
    }

    public Mission(MissionInfoSO missionInfo, MissionState missionState,
        int currentMissionStepIndex, MissionStepState[] missionStepStates) 
    { 

        this.missionInfo = missionInfo;
        this.missionState = missionState;
        this.currentMissionStepIndex=currentMissionStepIndex;
        this.missionStepStates=missionStepStates;

        if(this.missionStepStates.Length != this.missionInfo.missionStepPrefabs.Length) 
        {
            Debug.LogWarning("MissionSteps and MissionStepStates are of different lengths, data desynch");
        }
    }

    public void MoveToNextStep()
    {
        currentMissionStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentMissionStepIndex < missionInfo.missionStepPrefabs.Length);
    }

    public void InstantiateMissionStep(Transform parentTransform)
    {
        GameObject go = GetCurrentMissionStepPrefab();

        if (go != null)
        {
            MissionStep step = Object.Instantiate(go, parentTransform).GetComponent<MissionStep>();
            step.InitializeMissionStep(missionInfo.Id, currentMissionStepIndex, missionStepStates[currentMissionStepIndex].state);
        }
    }

    private GameObject GetCurrentMissionStepPrefab()
    {
        GameObject missionStepPrefab = null;
        if (CurrentStepExists())
        {
            missionStepPrefab = missionInfo.missionStepPrefabs[currentMissionStepIndex];
        }
        else
        {
            Debug.LogWarning("No mission step with index:" + currentMissionStepIndex
                + " for mission:" + missionInfo.name);
        }
        return missionStepPrefab;
    }

    public void StoreMissionStepState(MissionStepState missionStepState, int stepIndex)
    {
        if(stepIndex < missionStepStates.Length)
        {
            missionStepStates[stepIndex].state = missionStepState.state;
        }
        else
        {
            Debug.LogWarning("Index out of range: " + stepIndex);
        }
    }

    public MissionData GetMissionData()
    {
        return new MissionData(missionState, currentMissionStepIndex, missionStepStates);
    }
}
