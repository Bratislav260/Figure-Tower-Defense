using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AccommodationObject : MonoBehaviour, IColorful
{
    protected int Level;
    protected AccommodationTower tower;
    [SerializeField] protected AccommodationObjectStats[] objectStats;
    public SpriteRenderer spriteRenderer { get; private set; }
    [field: SerializeField] public Color color { get; set; }

    protected bool isAveable = false;

    protected Dictionary<AccommodationObjectStat, float> currentObjectStats = new Dictionary<AccommodationObjectStat, float>();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Level = objectStats[0].Level;
        currentObjectStats = objectStats[0].stats.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public virtual void SetStats() { }

    public virtual void Set(Vector3 setPosition)
    {
        transform.position = setPosition;
        spriteRenderer.enabled = true;
    }

    public void Accommodate(AccommodationTower tower)
    {
        this.tower = tower;
        SetStats();
        isAveable = true;
    }

    protected void ObjDestroyedSend()
    {
        tower?.StartObjectSet();
    }

    protected void GetBack()
    {
        transform.position = tower.transform.position;
        SetStats();
        isAveable = false;
        spriteRenderer.enabled = false;
    }

    public void UpgradeStats(int upgradeLevel)
    {
        foreach (var stat in objectStats[upgradeLevel - 1].stats)
        {
            currentObjectStats[stat.Key] = stat.Value;
        }

        Level = objectStats[upgradeLevel - 1].Level;
        SetStats();
    }
}
