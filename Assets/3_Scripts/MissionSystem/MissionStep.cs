using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionStep : MonoBehaviour
{
    private bool isFinished = false;
    private string missionId;
    private int stepIndex;

    public void InitializeMissionStep(string missionId, int stepIndex, string missionStepState)
    {
        this.missionId = missionId;
        this.stepIndex = stepIndex;

        if(missionStepState != null && missionStepState != "") 
        { 
            SetMissionStepState(missionStepState);
        }
    }

    protected void FinishMissionStep()
    {
        if(!isFinished)
        {
            isFinished = true;
            EventManager.Instance.AdvanceQuest(this.missionId);
            Destroy(gameObject);
        }
    }

    protected void ChangeState(string newState)
    {
        EventManager.Instance.MissionStepStateChange(missionId, stepIndex, new MissionStepState(newState));
    }

    protected abstract void SetMissionStepState(string state);
}
