using UnityEngine;

public class TowerTargeting
{
    public Transform launcher { get; private set; }
    private Vector2 direction;
    private float previousAngle;

    public TowerTargeting(Transform launcher)
    {
        this.launcher = launcher;
    }

    public void TargetingAim(Transform target, Transform transform)
    {
        direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        previousAngle = angle;

        if (previousAngle != transform.rotation.z)
            transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
