using UnityEngine;

public class SplashDamageBullet : Bullet
{
    private Transform target;
    [SerializeField] private float fireZoneEffectTime = 1f;
    [SerializeField] private Transform Firesquare;
    [SerializeField] LayerMask unitLayer;
    [SerializeField] private Vector2 boxSize = new Vector2(10f, 7f);

    public override void Effected(Transform shootedUnit)
    {
        Transform firedsquare = Instantiate(Firesquare, transform.position, Quaternion.identity);
        Collider2D[] firedUnits = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, unitLayer);

        foreach (Collider2D firedUnit in firedUnits)
        {
            if (firedUnit.TryGetComponent<Unit>(out var unit))
            {
                unit.GetDamage(damage);
            }
        }

        SoundSystem.Instance.Sound("Roket").Play();
        Destroy(firedsquare.gameObject, fireZoneEffectTime);
    }

    public override void Shoot(Transform target, float speed, float damage)
    {
        this.damage = damage;
        this.target = target;
        this.speed = speed;

        StartCoroutine(FollowTarget(this.target));
    }
}
