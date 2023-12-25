using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class UIMissionVisualizer : MonoBehaviour
{
    [SerializeField] private int missionStepCount;
    [SerializeField] private TextMeshProUGUI tfMissionText;
    [SerializeField] private TextMeshProUGUI tfDifficulty;
    [SerializeField] private TextMeshProUGUI tfCompleted;

    public void Setup(MissionStep missionStep, int currentMissionStep)
    {
        tfMissionText.text = missionStep.displayName;
        tfDifficulty.text = missionStep.difficulty.ToString();
        bool isCurrentMission = currentMissionStep == missionStepCount;
        bool notCompletedMission = currentMissionStep < missionStepCount;

        tfMissionText.color = isCurrentMission ? Color.blue : Color.black;


        tfDifficulty.gameObject.SetActive(currentMissionStep <= missionStepCount);
        tfMissionText.gameObject.SetActive(currentMissionStep <= missionStepCount);
        tfCompleted.gameObject.SetActive(currentMissionStep > missionStepCount);
    }
}
