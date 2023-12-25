using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionData
{
    public MissionState state;
    public int missionStepIndex;
    public MissionStepState[] missionStepStates;

    public MissionData(MissionState state, int missionStepIndex, MissionStepState[] missionStepStates)
    {
        this.state = state;
        this.missionStepIndex = missionStepIndex;
        this.missionStepStates = missionStepStates;
    }

}
