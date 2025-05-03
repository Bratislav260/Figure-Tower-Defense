using UnityEngine;

[RequireComponent(typeof(BossBehaviour), typeof(RoketLauncher), (typeof(Invisibality)))]
/// <summary>
/// Класс Босс
/// </summary>
public class TrapezoidBoss : Unit
{
    private BossBehaviour bossBehaviour;
    public RoketLauncher roketLauncher { get; private set; }
    public Invisibality invisibality { get; private set; }
    public new SpriteRenderer spriteRenderer { get; private set; }
    public bool isInvulnerable = false;

    public override void Initialize(float statModifierPercentages, float speed = 0, int pathPointIndex = 0)
    {
        base.Initialize(0, speed);
        spriteRenderer = GetComponent<SpriteRenderer>();

        bossBehaviour = GetComponent<BossBehaviour>();
        bossBehaviour.Initialize(this);

        roketLauncher = GetComponent<RoketLauncher>();
        roketLauncher.Initialize();

        invisibality = GetComponent<Invisibality>();
        invisibality.Initialize(this);
    }

    public override void GetDamage(float damage)
    {
        if (!isInvulnerable)
        {
            currentHitpoints -= damage;
            bossBehaviour.IsInvisibleMode();

            if (!isDead && currentHitpoints <= 0)
            {
                isDead = true;
                Dead();
            }
        }
    }

    public override void Dead()
    {
        UnitController.Instance.allUnitsList.Remove(this);
        ParticleManager.Instance.CallPartical("BossDestroy", transform, true);
        SoundSystem.Instance.Sound("Explove").Play();
        gameObject.SetActive(false);
        Destroy(gameObject, 5);
    }
}
