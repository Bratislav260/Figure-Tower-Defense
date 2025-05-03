using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "New StatBase", menuName = "ScriptableObjects/Stat")]
public class Stats : ScriptableObject
{
    [field: SerializeField] public int Level { get; private set; } = 1;
    [SerializedDictionary("Key", "Value")] public SerializedDictionary<Stat, float> stats;

    public float GetStat(Stat stat)
    {
        if (stats.TryGetValue(stat, out float value))
        {
            return value;
        }
        else
        {
            Debug.LogError("Нет такого STAT");
            return 0f;
        }
    }
}

public enum Stat
{
    hitPoints,
    cost,
    damage,
    fireSpeed,
    smallCooldown,
    bigCooldown,
    multiplyShootCooldown,
    maxTargetCount,
    maxBulletCount
}