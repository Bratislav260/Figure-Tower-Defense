using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс режима невидимости
/// </summary>
public class Invisibality : MonoBehaviour
{
    private TrapezoidBoss trapezoidBoss;
    public WaveTypeInfo currentWaveType;
    [SerializeField] private float unitSpawnTime = 0.5f;
    private float statModifierPercentages;
    Color spriteColor;

    public void Initialize(TrapezoidBoss trapezoidBoss)
    {
        this.trapezoidBoss = trapezoidBoss;
        spriteColor = trapezoidBoss.spriteRenderer.color;
    }

    public void StartInvisibality()
    {
        trapezoidBoss.isInvulnerable = true;
        spriteColor.a = 0.5f;
        trapezoidBoss.spriteRenderer.color = spriteColor;
        StartCoroutine(StartSpawn());
    }

    private void SpawnUnit(Unit unit, Vector2 position)
    {
        Unit spawnedUnit = Instantiate(unit, position, Quaternion.identity);
        spawnedUnit.Initialize(statModifierPercentages, speed: currentWaveType.waveSpeed, pathPointIndex: trapezoidBoss.unitMovement.pathPointIndex);
        SoundSystem.Instance.Sound("SpawnUnit").Play();
    }

    private IEnumerator StartSpawn()
    {
        List<Unit> unitsToSpawn = GetSpawnUnits();
        Shuffle(ref unitsToSpawn);
        float offset = 0;

        for (int i = 0; i < unitsToSpawn.Count; i++)
        {
            Vector2 unitPosition = new Vector2(transform.position.x + offset, transform.position.y);
            SpawnUnit(unitsToSpawn[i], unitPosition);

            offset -= 0.3f;
            yield return new WaitForSeconds(unitSpawnTime);
            // countSpawnedUnit--;
        }

        yield return StartCoroutine(ResetInvulnerable());
    }

    private List<Unit> GetSpawnUnits()
    {
        int currentUnitsAmount = 0;
        List<Unit> unitsToSpawn = new List<Unit>();

        while (currentWaveType.unitsAmount > currentUnitsAmount)
        {
            int chance = Random.Range(0, 101);
            foreach (var unitInfo in currentWaveType.unitsPercentages)
            {
                if (chance <= unitInfo.Value)
                {
                    unitsToSpawn.Add(unitInfo.Key);
                    currentUnitsAmount++;
                }
            }
        }

        return unitsToSpawn;
    }

    public void Shuffle(ref List<Unit> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            Unit temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private IEnumerator ResetInvulnerable()
    {
        yield return new WaitForSeconds(7);
        trapezoidBoss.isInvulnerable = false;
        spriteColor.a = 1f;
        trapezoidBoss.spriteRenderer.color = spriteColor;
        BossEventManager.MoveStateAction();
    }
}
