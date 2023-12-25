using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionGoal
{
    public MissionType missionType;

    public int goalAmmount;
    public int currentAmmount;

    public bool IsReached() { return (currentAmmount > goalAmmount); }
}

public enum MissionType
{
    ReachLevel,
    ReachCombo,
    ExplodeBarrels
}
