using UnityEngine;

public class SimpleUnit : Unit
{
    public override void Initialize(float statModifierPercentages, float speed = 0, int pathPointIndex = 0)
    {
        base.Initialize(statModifierPercentages, speed, pathPointIndex);
    }

    public override void UnitUpdate()
    {
        base.UnitUpdate();
    }

    public override void UnitFixedUpdate()
    {
        base.UnitFixedUpdate();
    }
}
