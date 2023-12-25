using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Mission", menuName ="Mission", order = 1)]
[System.Serializable]
public class MissionInfoSO: ScriptableObject
{
    [SerializeField] public string Id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public MissionInfoSO[] missionPrerequisite;
    public GameObject[] missionStepPrefabs;


    private void OnValidate()
    {
        #if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}

public enum MissionStepDifficulty
{
    EASY,
    NORMAL,
    HARD
}