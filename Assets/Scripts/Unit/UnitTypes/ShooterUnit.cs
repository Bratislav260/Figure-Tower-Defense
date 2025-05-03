using System.Collections.Generic;
using UnityEngine;

public class ShooterUnit : Unit, IShootable
{
    #region --- Подклассы ---

    [Header("Подклассы")]
    private Shooter unitShooter;
    private Locator locator;
    [SerializeField] private Bullet bulletType;
    #endregion

    #region --- Параметры ---
    [Tooltip("Stats")]
    public bool isAvableAttack { get; set; }
    [field: SerializeField] public float damage { get; set; }
    [field: SerializeField] public float fireSpeed { get; set; }
    [field: SerializeField] public float smallCooldown { get; set; }
    [field: SerializeField] public float bigCooldown { get; set; }
    [field: SerializeField] public float maxTargetCount { get; set; }
    [field: SerializeField] public float maxBulletCount { get; set; }
    [field: SerializeField] public float currentBulletCount { get; set; }
    [field: SerializeField] public float multiplyShootCooldown { get; set; }
    #endregion

    private List<Transform> attackTargets = new List<Transform>();

    public override void Initialize(float statModifierPercentages, float speed = 0, int pathPointIndex = 0)
    {
        base.Initialize(statModifierPercentages, speed, pathPointIndex);
        unitShooter = GetComponent<Shooter>();
        locator = new Locator();

        unitShooter.Initialize(this);
    }

    public override void UnitFixedUpdate()
    {
        base.UnitFixedUpdate();
    }

    public override void UnitUpdate()
    {
        base.UnitUpdate();
        attackTargets = locator.LocateTargets(transform, unitShooter.attackRadius, unitShooter.unitLayer, maxTargetCount);

        for (int i = 0; i < attackTargets.Count; i++)
        {
            unitShooter.Shoot(bulletType, attackTargets);
        }
    }
}
