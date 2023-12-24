using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Mission", menuName ="Mission", order = 1)]
[System.Serializable]
public class Mission : ScriptableObject
{
    public string description;
    public MissionGoal goal;
    public Difficulty difficulty;
}

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}
