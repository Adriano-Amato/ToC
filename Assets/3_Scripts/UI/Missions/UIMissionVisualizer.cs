using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class UIMissionVisualizer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tfMissionText;
    [SerializeField] private TextMeshProUGUI tfDifficulty;
    [SerializeField] private TextMeshProUGUI tfCompleted;

    public void Setup(Mission m)
    {
        tfMissionText.text = m.missionInfo.displayName;
        tfDifficulty.text = m.missionInfo.difficulty.ToString();

        tfDifficulty.gameObject.SetActive(m.missionState is not MissionState.FINISHED);
        tfMissionText.gameObject.SetActive(m.missionState is not MissionState.FINISHED);
        tfCompleted.gameObject.SetActive(m.missionState is MissionState.FINISHED);
    }
}
