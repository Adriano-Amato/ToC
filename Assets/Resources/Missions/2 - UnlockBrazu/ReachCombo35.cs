using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachCombo35: MissionStep
{
    private int currentCombo = 0;
    private int comboToComplete = 35;

    private void OnEnable()
    {
        EventManager.Instance.OnComboChanged += ComboSet;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnComboChanged -= ComboSet;
    }

    private void ComboSet(int combo)
    {
        if(currentCombo < comboToComplete)
        {
            currentCombo = combo;
            UpdateState();
        }

        if (currentCombo >= comboToComplete)
        {
            FinishMissionStep();
        }
    }

    private void UpdateState()
    {
        string state = currentCombo.ToString();
        ChangeState(state);
    }

    protected override void SetMissionStepState(string state)
    {
        this.currentCombo = 0;
        UpdateState();
    }
}
