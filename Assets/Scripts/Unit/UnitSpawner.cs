using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private List<WaveTypeInfo> wavesList = new List<WaveTypeInfo>();
    private WaveTypeInfo currentWaveType;
    [SerializeField] private AnimationCurve levelUpPercentages;
    public int currentWave { get; private set; } = 0;
    [SerializeField] private float maxWave;
    [SerializeField] private float unitSpawnTime = 0.5f;
    [SerializeField] private float unitWaveTime = 5f;
    private float statModifierPercentages;

    public void Initialize()
    {
        StartCoroutine(Wave());

        EventManager.onStopWave.AddListener(InterruptWave);
    }

    private IEnumerator Wave()
    {
        while (true)
        {
            yield return new WaitForSeconds(unitWaveTime);

            currentWave++;
            if (currentWave > maxWave)
            {
                EventManager.LastWave();
                StopAllCoroutines();
                yield break;
            }

            LevelUpUnits(currentWave);
            NewWaveStart();

            SoundSystem.Instance.Sound("NewWave").Play();
            UIEventManager.WaveUIUpdate(currentWave);
        }
    }

    private void NewWaveStart()
    {
        WaveChoose();
        if (currentWaveType.WaveType == WaveType.BossWave)
        {
            SoundSystem.Instance.StopLastPlayedMusics();
            SoundSystem.Instance.Music("BossFightMusic").Play();
        }

        StartCoroutine(StartSpawn());
    }

    private void SpawnUnit(Unit unit, Vector2 position)
    {
        Unit spawnedUnit = Instantiate(unit, position, Quaternion.identity);
        spawnedUnit.Initialize(statModifierPercentages, speed: currentWaveType.waveSpeed);
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
        }
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

    public void InterruptWave()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Выбар вида волны
    /// </summary>
    private void WaveChoose()
    {
        if (currentWave == 1)
        {
            currentWaveType = wavesList.Find(x => x.WaveType == WaveType.Firstwave);
        }
        else if (currentWave == 5)
        {
            currentWaveType = wavesList.Find(x => x.WaveType == WaveType.HeavyWave);
        }
        else if (currentWave == 10)
        {
            currentWaveType = wavesList.Find(x => x.WaveType == WaveType.BossWave);
        }
        else
        {
            List<WaveTypeInfo> normalWaves = wavesList.FindAll(x => x.WaveType == WaveType.NormalWave);

            if (normalWaves.Count > 0)
            {
                int randomIndex = Random.Range(0, normalWaves.Count);
                currentWaveType = normalWaves[randomIndex];
            }
        }
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

    /// <summary>
    /// Увеличение статов юнитов
    /// </summary>
    private void LevelUpUnits(int waveCount)
    {
        statModifierPercentages = levelUpPercentages.Evaluate(waveCount);
    }
}
