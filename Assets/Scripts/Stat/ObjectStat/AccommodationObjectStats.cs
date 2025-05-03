using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "New Accommodation Object Stat", menuName = "ScriptableObjects/Accommodation Object Stat")]
public class AccommodationObjectStats : ScriptableObject
{
    [field: SerializeField] public int Level { get; private set; } = 1;
    [SerializedDictionary("Key", "Value")] public SerializedDictionary<AccommodationObjectStat, float> stats;

    public float GetStat(AccommodationObjectStat stat)
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

public enum AccommodationObjectStat
{
    hitPoints,
    damage,
    bigCooldown,
    damageRadius
}