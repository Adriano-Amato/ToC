using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tfTitle, tfSubtitle;
    [SerializeField] private UIMissionVisualizer easyMission;
    [SerializeField] private UIMissionVisualizer normalMission;
    [SerializeField] private UIMissionVisualizer hardMission;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        EventManager.Instance.onMissionStateChange += SetupMissions;
        EventManager.Instance.onMissionsLoad += SetupMissions;
    }

    private void OnDisable()
    {
        EventManager.Instance.onMissionStateChange -= SetupMissions;
        EventManager.Instance.onMissionsLoad -= SetupMissions;
    }

    private void SetupMissions(string missionId)
    {
        Mission mission = GameManager.Instance.missionManager.GetMissionById(missionId);
        var missionSteps = mission.missionInfo.missionStepPrefabs;
        MissionStep missionStep;

        foreach( var step in missionSteps)
        {
            missionStep = step.GetComponent<MissionStep>();

            switch(missionStep.difficulty)
            {
                case MissionStepDifficulty.EASY:
                    easyMission.Setup(missionStep, mission.currentMissionStepIndex);
                    break;
                case MissionStepDifficulty.NORMAL:
                    normalMission.Setup(missionStep, mission.currentMissionStepIndex);
                    break;
                case MissionStepDifficulty.HARD:
                    hardMission.Setup(missionStep, mission.currentMissionStepIndex);
                    break;
                
            }
        }
    }

    private void SetupMissions(Mission mission)
    {
        var missionSteps = mission.missionInfo.missionStepPrefabs;
        MissionStep missionStep;

        foreach (var step in missionSteps)
        {
            missionStep = step.GetComponent<MissionStep>();

            switch (missionStep.difficulty)
            {
                case MissionStepDifficulty.EASY:
                    easyMission.Setup(missionStep, mission.currentMissionStepIndex);
                    break;
                case MissionStepDifficulty.NORMAL:
                    normalMission.Setup(missionStep, mission.currentMissionStepIndex);
                    break;
                case MissionStepDifficulty.HARD:
                    hardMission.Setup(missionStep, mission.currentMissionStepIndex);
                    break;

            }
        }
    }

    public void PlayOpenAnim()
    {
        if(!animator.GetBool("shouldOpen"))
            animator.SetBool("shouldOpen", true);
    }

    public void PlayCloseAnim()
    {
        animator.SetBool("shouldOpen", false);
    }
}
