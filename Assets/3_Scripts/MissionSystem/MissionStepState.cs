using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionStepState
{
    public string state;

    public MissionStepState(string state)
    {
        this.state = state;
    }

    public MissionStepState()
    {
        this.state="";
    }
}
