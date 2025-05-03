using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "New Wave", menuName = "ScriptableObjects/Wave")]
public class WaveTypeInfo : ScriptableObject
{
    [field: SerializeField] public WaveType WaveType { get; private set; }
    [field: SerializeField] public float waveSpeed { get; private set; }
    [field: SerializeField] public int unitsAmount { get; private set; }
    [SerializedDictionary("Units", "Percentages")] public SerializedDictionary<Unit, int> unitsPercentages;
}

public enum WaveType
{
    Firstwave,
    NormalWave,
    HeavyWave,
    BossWave
}