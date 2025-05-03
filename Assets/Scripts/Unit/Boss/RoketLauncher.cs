using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
/// <summary>
/// Класс атаки Босса
/// </summary>
public class RoketLauncher : MonoBehaviour, IShootable
{
    [SerializeField] private Bullet bulletType;
    private List<Transform> attackTargets = new List<Transform>();
    private Shooter shooter;
    private Locator locator;

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

    public void Initialize()
    {
        locator = new Locator();
        shooter = GetComponent<Shooter>();
        shooter.Initialize(this);
    }

    public void StartLauncher()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        int targetTowers = 0;
        int iterations = 0;

        while (targetTowers <= maxTargetCount && iterations < 20)
        {
            if (shooter.isAvableLocate)
            {
                attackTargets = locator.LocateTargets(transform, shooter.attackRadius, shooter.unitLayer, maxTargetCount);
            }

            shooter.Shoot(bulletType, attackTargets);
            targetTowers += attackTargets.Count;
            iterations++;

            yield return null;
        }

        yield return new WaitForSeconds(3);
        BossEventManager.MoveStateAction();
    }
}
