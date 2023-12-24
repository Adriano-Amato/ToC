using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tfTitle, tfSubtitle;
    [SerializeField] List<UIMissionVisualizer> missions;
    [SerializeField] Animation openContent;

    private void Awake()
    {
        openContent?.Play();

        foreach(var mission in missions)
        {

        }
    }
}
