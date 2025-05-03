// using UnityEditor;
using UnityEngine;

public class BomberUnit : Unit
{
    [SerializeField] private float bombDamage;
    [SerializeField, Range(0, 4)] private float damageRadius;
    [SerializeField] private LayerMask layerMask;

    private void Explovebomb()
    {
        Collider2D[] explovedTowers = Physics2D.OverlapCircleAll(transform.position, damageRadius, layerMask);

        foreach (var explovedTower in explovedTowers)
        {
            if (explovedTower.TryGetComponent<Tower>(out var tower))
            {
                tower.GetDamage(bombDamage);
            }
        }
    }

    public override void Dead()
    {
        base.Dead();
        Explovebomb();
    }

    // private void OnDrawGizmos()
    // {
    //     Handles.color = Color.red;
    //     Handles.DrawWireDisc(transform.position, transform.forward, damageRadius);
    // }
}
