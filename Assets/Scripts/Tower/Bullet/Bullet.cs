using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected new Rigidbody2D rigidbody;
    [field: SerializeField] public SpriteRenderer spriteRenderer { get; private set; }
    protected float damage;
    protected float speed;
    protected float destroyDistance = 0.1f;
    private Vector2 direction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void Shoot(Transform target, float speed, float damage);

    public abstract void Effected(Transform unit);

    public IEnumerator FollowTarget(Transform target)
    {
        while (true)
        {
            if (target == null)
            {
                StartCoroutine(MoveToDestroyedTarget(target));
                yield break;
            }

            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= destroyDistance)
            {
                Effected(target);
                Destroy(gameObject);
                // Debug.Log("Destroyed");
                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            TargetingAim(target);

            yield return null;
        }
    }

    private void TargetingAim(Transform target)
    {
        direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private IEnumerator MoveToDestroyedTarget(Transform destroyedTarget)
    {
        Vector3 targetPos = destroyedTarget.position;
        while (Vector3.Distance(transform.position, targetPos) >= destroyDistance)
        {
            Destroy(gameObject);
            yield return null;
        }

        yield break;
    }
}
