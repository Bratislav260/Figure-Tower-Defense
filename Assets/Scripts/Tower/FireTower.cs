using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower, IShootable
{
    #region --- Подклассы ---

    [Header("Подклассы")]
    public Shooter towerShooter;
    private TowerTargeting towerTargeting;
    private Locator locator;
    [SerializeField] private Bullet bulletType;
    #endregion

    #region --- UI ---
    [Header("UI")]
    public bool IsHasBulletSprite;
    public bool IsHasChildrenSprite;
    public bool IsHasLauncher;
    [HideInInspector] public Transform _launcher;
    #endregion

    #region --- STATS ---
    public bool isAvableAttack { get; set; }
    public float damage { get; set; }
    public float fireSpeed { get; set; }
    public float smallCooldown { get; set; }
    public float bigCooldown { get; set; }
    public float multiplyShootCooldown { get; set; }
    public float maxTargetCount { get; set; }
    public float maxBulletCount { get; set; }
    #endregion

    private List<Transform> attackTargets = new List<Transform>();

    public override void Initialize()
    {
        base.Initialize();

        towerShooter = GetComponent<Shooter>();
        locator = new Locator();

        if (IsHasLauncher)
        {
            towerTargeting = new TowerTargeting(_launcher);
        }

        towerShooter.Initialize(this, towerTargeting);
        spriteController.Initialize(bulletType, _launcher);
    }

    public override void SetTowerStats()
    {
        base.SetTowerStats();

        damage = currentTowerStats[Stat.damage];
        fireSpeed = currentTowerStats[Stat.fireSpeed];
        smallCooldown = currentTowerStats[Stat.smallCooldown];
        bigCooldown = currentTowerStats[Stat.bigCooldown];
        multiplyShootCooldown = currentTowerStats[Stat.multiplyShootCooldown];
        maxTargetCount = currentTowerStats[Stat.maxTargetCount];
        maxBulletCount = currentTowerStats[Stat.maxBulletCount];
    }

    public override void TowerUpdate()
    {
        Attack();
    }

    private void Attack()
    {
        if (towerShooter.isAvableLocate)
        {
            attackTargets = locator.LocateTargets(transform, towerShooter.attackRadius, towerShooter.unitLayer, maxTargetCount);
        }
        towerShooter.Shoot(bulletType, attackTargets, IsHasLauncher);
    }

    public override void SetNewSprite()
    {
        spriteController.SpriteChange(spriteRenderer, Level, IsHasBulletSprite, IsHasChildrenSprite);
    }
}
