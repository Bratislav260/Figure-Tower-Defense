using System;
using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(Drop))]
public abstract class Unit : MonoBehaviour, IDamageable, IColorful
{
    [field: SerializeField] public UnitMovement unitMovement { get; private set; }
    [field: SerializeField] public Stats stats { get; private set; }
    public Drop drop { get; private set; }

    [field: SerializeField] public float currentHitpoints { get; set; }
    [field: SerializeField] public Color color { get; set; }
    protected SpriteRenderer spriteRenderer;

    private new Rigidbody2D rigidbody;
    [SerializeField] private float moveSpeed;
    protected bool isDead = false;

    public virtual void Initialize(float statModifierPercentages, float speed = 0, int pathPointIndex = 0)
    {
        UnitController.Instance.allUnitsList.Add(this);
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        moveSpeed = speed + speed * (statModifierPercentages / 100);

        unitMovement = GetComponent<UnitMovement>();
        unitMovement.Initialize(rigidbody, moveSpeed, pathPointIndex);
        drop = GetComponent<Drop>();

        SetStats(statModifierPercentages);
    }

    private void SetStats(float statModifierPercentages)
    {
        currentHitpoints = stats.GetStat(Stat.hitPoints) + (stats.GetStat(Stat.hitPoints) * (statModifierPercentages / 50));
    }

    public virtual void UnitUpdate()
    {
        if (transform)
        {
            unitMovement.PathPointUpdate();
        }
    }

    public virtual void UnitFixedUpdate()
    {
        if (transform)
        {
            unitMovement.Move();
        }
    }

    public virtual void GetDamage(float damage)
    {
        currentHitpoints -= damage;

        if (!isDead && currentHitpoints <= 0)
        {
            isDead = true;
            Dead();
        }
    }

    public virtual void Dead()
    {
        UnitController.Instance.allUnitsList.Remove(this);
        drop.Droping();
        ParticleManager.Instance.CallPartical("Destroy", transform, true);
        SoundSystem.Instance.Sound("UnitDead").Play();
        gameObject.SetActive(false);
        Destroy(gameObject, 5);
    }
}
