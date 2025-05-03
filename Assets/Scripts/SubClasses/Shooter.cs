using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
// using UnityEditor;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private IShootable shootable;
    private TowerTargeting towerTargeting;
    [SerializeField] private Transform firePoint;

    #region --- Параметры ---
    [field: SerializeField, Range(0, 15)] public float attackRadius { get; private set; }
    [field: SerializeField] public LayerMask unitLayer { get; private set; }
    public bool isAvableAttack { get; private set; } = true;
    public bool isAvableLocate { get; private set; } = true;
    private float currentBulletCount;
    public List<Transform> provokedTargets { get; private set; } = new List<Transform>();
    [SerializeField] private string ShootSound;
    #endregion

    public List<Bullet> bullets { get; private set; } = new List<Bullet>();

    public void Initialize(IShootable shootable, TowerTargeting towerTargeting = null)
    {
        this.shootable = shootable;
        currentBulletCount = shootable.maxBulletCount;
        this.towerTargeting = towerTargeting;
    }

    public async void Shoot(Bullet bulletType, List<Transform> attackTargets, bool IsHasLauncher = false)
    {
        if (isAvableAttack && currentBulletCount > 0)
        {
            if (provokedTargets.Count > 0)
            {
                isAvableLocate = false;
                await BulletRelease(bulletType, provokedTargets.GetRange(0, 1), IsHasLauncher);

                if (currentBulletCount <= 0)
                {
                    isAvableAttack = false;
                    currentBulletCount = shootable.maxBulletCount;
                    StartCoroutine(BigCooldown());
                }
                else
                {
                    isAvableAttack = false;
                    StartCoroutine(SmallCooldown());
                }
            }

            else if (attackTargets.Count > 0)
            {
                isAvableLocate = false;
                await BulletRelease(bulletType, attackTargets, IsHasLauncher);

                if (currentBulletCount <= 0)
                {
                    attackTargets.Clear();
                    isAvableAttack = false;
                    currentBulletCount = shootable.maxBulletCount;

                    if (gameObject.activeInHierarchy)
                        StartCoroutine(BigCooldown());
                }
                else
                {
                    isAvableAttack = false;

                    if (gameObject.activeInHierarchy)
                        StartCoroutine(SmallCooldown());
                }
            }
        }
    }

    private async Task BulletRelease(Bullet bulletType, List<Transform> attackTargets, bool IsHasLauncher)
    {
        for (int i = 0; i < attackTargets.Count; i++)
        {
            if (IsHasLauncher && attackTargets[i] && isAvableAttack)
            {
                towerTargeting.TargetingAim(attackTargets[i], towerTargeting.launcher);
            }

            Bullet shootedBullet = Instantiate(bulletType, firePoint.position, firePoint.rotation);
            bullets.Add(shootedBullet);
            shootedBullet.Shoot(attackTargets[i], shootable.fireSpeed, shootable.damage);

            currentBulletCount -= 1;
            SoundSystem.Instance.Sound(ShootSound).Play();
            await Task.Delay((int)shootable.multiplyShootCooldown);
        }
    }

    private IEnumerator BigCooldown()
    {
        yield return new WaitForSeconds(shootable.bigCooldown);
        isAvableAttack = true;
        isAvableLocate = true;
    }

    private IEnumerator SmallCooldown()
    {
        yield return new WaitForSeconds(shootable.smallCooldown);
        isAvableAttack = true;
    }

    // private void OnDrawGizmos()
    // {
    //     Handles.color = Color.red;
    //     Handles.DrawWireDisc(transform.position, transform.forward, attackRadius);
    // }
}
