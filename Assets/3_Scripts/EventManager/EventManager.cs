using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action OnBallShot;
    public void BallShot()
    {
        if (OnBallShot != null)
        {
            OnBallShot();
        }
    }

    public event Action<int> OnComboChanged;
    public void ComboChanged(int combo)
    {
        if(OnComboChanged != null)
        {
            OnComboChanged(combo);
        }
    }

    public event Action<int> OnBarrelExploded;
    public void BarrelExploded(int barrel)
    {
        if(OnBarrelExploded != null)
        {
            OnBarrelExploded(barrel);
        }
    }

    public event Action<string> onStartMission;
    public void StartMission(string id)
    {
        if (onStartMission != null)
        {
            onStartMission(id);
        }
    }

    public event Action<string> onAdvanceMission;
    public void AdvanceQuest(string id)
    {
        if(onAdvanceMission != null)
        {
            onAdvanceMission(id);
        }
    }

    public event Action<string> onMissionsLoad;
    public void MissionsLoad(string id)
    {
        if (onMissionsLoad != null)
        {
            onMissionsLoad(id);
        }
    }

    public event Action<string> onFinishMission;
    public void FinishMission(string id)
    {
        if(onFinishMission != null)
        { 
            onFinishMission(id); 
        }
    }

    public event Action<Mission> onMissionStateChange;
    public void MissionStateChange(Mission mission)
    {
        if(onMissionStateChange != null)
        { 
            onMissionStateChange(mission); 
        }
    }

    public event Action<string, int, MissionStepState> onMissionStepStateChange;
    public void MissionStepStateChange(string id, int stepIndex, MissionStepState stepState)
    {
        if(onMissionStepStateChange != null)
        {
            onMissionStepStateChange(id, stepIndex, stepState);
        }
    }
}
