using UnityEngine;

public interface IDamageable
{
    float currentHitpoints { get; set; }

    public void GetDamage(float bulletDamage) { }
}
