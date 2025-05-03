using UnityEngine;

public interface IShootable
{
    public bool isAvableAttack { get; set; }
    public float damage { get; set; }
    public float fireSpeed { get; set; }
    public float smallCooldown { get; set; }
    public float bigCooldown { get; set; }
    public float multiplyShootCooldown { get; set; }
    public float maxTargetCount { get; set; }
    public float maxBulletCount { get; set; }
}
