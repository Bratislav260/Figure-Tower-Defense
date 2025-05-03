using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class Tower : MonoBehaviour, IDamageable, IColorful
{
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Stats stats { get; private set; }
    protected SpriteController spriteController;
    public SpriteRenderer spriteRenderer { get; private set; }

    protected Dictionary<Stat, float> currentTowerStats = new Dictionary<Stat, float>();

    public float currentHitpoints { get; set; }
    public float towerCost { get; private set; }

    [field: SerializeField] public Color color { get; set; }
    private bool isDead = false;

    public virtual void Initialize()
    {
        TowerController.Instance.allTowersList.Add(this);
        spriteController = GetComponent<SpriteController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Level = stats.Level;
        currentTowerStats = stats.stats.ToDictionary(entry => entry.Key, entry => entry.Value);
        SetTowerStats();
    }

    public virtual void TowerUpdate() { }

    public virtual void SetTowerStats()
    {
        currentHitpoints = currentTowerStats[Stat.hitPoints];
        towerCost = currentTowerStats[Stat.cost];
    }

    public void UpgradeStats(Stats upgradeStat)
    {
        foreach (var stat in upgradeStat.stats)
        {
            currentTowerStats[stat.Key] = stat.Value;
        }

        Level = upgradeStat.Level;
        SetNewSprite();
    }

    public virtual void SetNewSprite()
    {
        spriteController.SpriteChange(spriteRenderer, Level);
    }

    public void GetDamage(float damage)
    {
        currentHitpoints -= damage;

        if (!isDead && currentHitpoints <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
        TowerController.Instance.Remove(this);
        ParticleManager.Instance.CallPartical("Destroy", transform, true);
        SoundSystem.Instance.Sound("TowerDestroy").Play();
        gameObject.SetActive(false);
        Destroy(gameObject, 5);
    }
}
