using System.Collections.Generic;
using UnityEngine;

public class Locator
{
    public List<Transform> LocateTargets(Transform transform, float radius, LayerMask layerMask, float maxTargetCount = 1)
    {
        List<Transform> attackTargets = new List<Transform>();
        Collider2D[] locatedUnits = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        int count = 0;

        foreach (var unit in locatedUnits)
        {
            if (unit != null)
            {
                attackTargets.Add(unit.transform);
                count += 1;
            }

            if (count >= maxTargetCount)
            {
                return attackTargets;
            }
        }

        return attackTargets;
    }
}
