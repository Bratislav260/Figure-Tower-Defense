using UnityEngine;

public class SimpleBullet : Bullet
{
    private Transform target;

    public override void Effected(Transform shootedUnit)
    {
        if (shootedUnit.TryGetComponent<IDamageable>(out var unit))
        {
            unit.GetDamage(damage);
        }
    }

    public override void Shoot(Transform target, float speed, float damage)
    {
        this.damage = damage;
        this.target = target;
        this.speed = speed;

        StartCoroutine(FollowTarget(this.target));
    }
}
