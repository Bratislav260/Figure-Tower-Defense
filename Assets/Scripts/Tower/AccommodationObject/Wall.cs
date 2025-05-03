using System.Collections;
using System.Linq;
using UnityEngine;

public class Wall : AccommodationObject
{
    private float hitPoints;
    private bool isWallBreaking = false;
    private Coroutine coroutine;

    public override void SetStats()
    {
        hitPoints = currentObjectStats[AccommodationObjectStat.hitPoints];
    }

    public override void Set(Vector3 setPosition)
    {
        StartCoroutine(IsNoUnits(setPosition));
    }

    private IEnumerator IsNoUnits(Vector3 setPosition)
    {
        Collider2D[] units;

        yield return new WaitUntil(() =>
        {
            units = Physics2D.OverlapBoxAll(setPosition, new Vector2(1.3f, 0.5f), 0);
            if (!units.Any(unit => unit.TryGetComponent<Unit>(out _)))
            {
                base.Set(setPosition);
                SoundSystem.Instance.Sound("WallPlaced").Play();
                return true;
            }
            return false;
        });
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAveable && collision.TryGetComponent<Unit>(out var unit))
        {
            unit.unitMovement.StopMove();
            isWallBreaking = true;
            coroutine = StartCoroutine(BreakingWall());

            if (!SoundSystem.Instance.Sound("WallBreaking").isPlaying)
                SoundSystem.Instance.Sound("WallBreaking").Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out var unit))
        {
            unit.unitMovement.StartMove();
            StopCoroutine(coroutine);
            isWallBreaking = false;
            SoundSystem.Instance.Sound("WallBreaking").Stop();
        }
    }

    private IEnumerator BreakingWall()
    {
        while (isWallBreaking)
        {
            yield return new WaitForSeconds(1);
            GetDamage();
        }
    }

    private void GetDamage()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            ParticleManager.Instance.CallPartical("Destroy", transform, true);
            SoundSystem.Instance.Sound("WallDestroyed").Play();
            ObjDestroyedSend();
            GetBack();
        }
    }
}
