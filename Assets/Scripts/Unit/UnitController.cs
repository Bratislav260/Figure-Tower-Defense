using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance { get; private set; }
    public List<Unit> allUnitsList = new List<Unit>();
    private bool isLastWave = false;

    public void Initialize()
    {
        Instance = this;

        EventManager.onLastWave.AddListener(LastWaveMode);
        EventManager.onStopWaveMove.AddListener(StopUnitsMove);
    }

    public void UnitsUpdate()
    {
        if (isLastWave && allUnitsList.Count <= 0)
        {
            EventManager.GameWin();
        }

        foreach (var unit in allUnitsList)
        {
            unit.UnitUpdate();
        }
    }

    public void UnitsFixedUpdate()
    {
        foreach (var unit in allUnitsList)
        {
            unit.UnitFixedUpdate();
        }
    }

    private void StopUnitsMove()
    {
        foreach (var unit in allUnitsList)
        {
            unit.unitMovement.StopMove();
        }
    }

    private void LastWaveMode()
    {
        isLastWave = true;
    }
}
