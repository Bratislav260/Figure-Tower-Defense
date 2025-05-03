// using UnityEditor;
using UnityEngine;

public class Bomb : AccommodationObject
{
    private float bombDamage;
    private float bombTime;
    private float damageRadius;
    [SerializeField] private LayerMask layerMask;

    public override void SetStats()
    {
        bombDamage = currentObjectStats[AccommodationObjectStat.damage];
        bombTime = currentObjectStats[AccommodationObjectStat.bigCooldown];
        damageRadius = currentObjectStats[AccommodationObjectStat.damageRadius];
    }

    private void Explove()
    {
        Collider2D[] explovedUnits = Physics2D.OverlapCircleAll(transform.position, damageRadius, layerMask);

        foreach (var explovedUnit in explovedUnits)
        {
            if (explovedUnit.TryGetComponent<Unit>(out var unit))
            {
                unit.GetDamage(bombDamage);
            }
        }

        ParticleManager.Instance.CallPartical("Explove", transform, true);
        SoundSystem.Instance.Sound("Explove").Play();
        ObjDestroyedSend();
        GetBack();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAveable && collision.TryGetComponent<Unit>(out var unit))
        {
            isAveable = false;
            Invoke(nameof(Explove), bombTime);
            SoundSystem.Instance.Sound("BombClick").Play();
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Handles.color = Color.red;
    //     Handles.DrawWireDisc(transform.position, transform.forward, damageRadius);
    // }
}
