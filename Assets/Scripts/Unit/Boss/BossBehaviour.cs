using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс поведение логики Босса
/// </summary>
public class BossBehaviour : MonoBehaviour
{
    private enum BossState
    {
        Move,
        Shoot,
        Invisible
    }

    private TrapezoidBoss trapezoidBoss;
    private List<BossState> bossStates;
    private BossState currentState;

    private bool IsInvisibleModed = false;

    public void Initialize(TrapezoidBoss trapezoidBoss)
    {
        BossEventManager.onStateFinish.AddListener(StartMove);
        this.trapezoidBoss = trapezoidBoss;

        currentState = BossState.Move;
        bossStates = new List<BossState>()
        {
            BossState.Shoot,
        };

        StartCoroutine(ChangingState());
    }

    public IEnumerator ChangingState()
    {
        yield return new WaitForSeconds(Random.Range(7, 11));

        currentState = bossStates[Random.Range(0, bossStates.Count)];
        SwitchState();
    }

    private void SwitchState()
    {
        trapezoidBoss.unitMovement.StopMove();
        switch (currentState)
        {
            case BossState.Move:
                trapezoidBoss.unitMovement.StartMove();
                // Debug.Log("BossState.Move");
                break;
            case BossState.Shoot:
                trapezoidBoss.roketLauncher.StartLauncher();
                // Debug.Log("BossState.Shoot");
                break;
            case BossState.Invisible:
                trapezoidBoss.invisibality.StartInvisibality();
                // Debug.Log("BossState.Invisible");
                break;
            default:
                trapezoidBoss.unitMovement.StartMove();
                break;
        }
    }

    public void IsInvisibleMode()
    {
        if (!IsInvisibleModed && trapezoidBoss.currentHitpoints <= (trapezoidBoss.stats.GetStat(Stat.hitPoints) / 2))
        {
            IsInvisibleModed = true;
            currentState = BossState.Invisible;
            SwitchState();
        }
    }

    private void StartMove()
    {
        currentState = BossState.Move;
        SwitchState();
    }
}
