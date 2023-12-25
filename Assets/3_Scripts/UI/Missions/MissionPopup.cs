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
    [SerializeField] private Animation contentAnimation;
    [SerializeField] private AnimationClip closeContent;
    [SerializeField] private AnimationClip openContent;

    private void OnEnable()
    {
        EventManager.Instance.onStartMission += SetupMissions;
        EventManager.Instance.onFinishMission += SetupMissions;
        EventManager.Instance.onMissionsLoad += SetupMissions;
    }

    private void OnDisable()
    {
        EventManager.Instance.onStartMission -= SetupMissions;
        EventManager.Instance.onFinishMission -= SetupMissions;
        EventManager.Instance.onMissionsLoad -= SetupMissions;
    }

    private void SetupMissions(string missionId)
    {
        Mission mission = GameManager.Instance.missionManager.GetMissionById(missionId);
       
        switch (mission.missionInfo.difficulty) 
        {
            case MissionDifficulty.EASY:
                easyMission.Setup(mission);
                break;
            case MissionDifficulty.NORMAL:
                normalMission.Setup(mission);
                break;
            case MissionDifficulty.HARD:
                hardMission.Setup(mission);
                break;
        }
    }

    public void PlayOpenAnim()
    {
        contentAnimation.clip = openContent;
        contentAnimation.Play();
    }

    public void PlayCloseAnim()
    {
        contentAnimation.clip = closeContent;
        contentAnimation.Play();
    }
}
