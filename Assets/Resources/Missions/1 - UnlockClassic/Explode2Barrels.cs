using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode2Barrels: MissionStep
{
    private int barrelsExploded = 0;
    private int barrelsToExplode = 2;

    private void OnEnable()
    {
        EventManager.Instance.OnBarrelExploded += BarrelSet;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnBarrelExploded -= BarrelSet;
    }

    private void BarrelSet(int numberOfExploded)
    {
        if(barrelsExploded < barrelsToExplode)
        {
            barrelsExploded += numberOfExploded;
            UpdateState();
        }

        if (barrelsExploded >= barrelsToExplode)
        {
            FinishMissionStep();
        }
    }

    private void UpdateState()
    {
        string state = barrelsExploded.ToString();
        ChangeState(state);
    }

    protected override void SetMissionStepState(string state)
    {
        this.barrelsExploded = System.Int32.Parse(state);
        UpdateState();
    }
}
