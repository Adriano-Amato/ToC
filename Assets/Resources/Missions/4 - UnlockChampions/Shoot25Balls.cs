using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot25Balls : MissionStep
{
    private int ballsShot = 0;
    private int ballsToShoot = 25;

    private void OnEnable()
    {
        EventManager.Instance.OnBallShot += BallShot;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnBallShot -= BallShot;
    }

    private void BallShot()
    {

        if (ballsShot < ballsToShoot)
        {
            ballsShot++;
            UpdateState();
        }

        if (ballsShot >= ballsToShoot)
        {
            FinishMissionStep();
        }
    }

    private void UpdateState()
    {
        string state = ballsShot.ToString();
        ChangeState(state);
    }

    protected override void SetMissionStepState(string state)
    {
        this.ballsShot = System.Int32.Parse(state);
        UpdateState();
    }
}
