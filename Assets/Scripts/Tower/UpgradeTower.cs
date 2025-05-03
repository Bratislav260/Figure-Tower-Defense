using UnityEngine;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

public class TowerUpgradeController : MonoBehaviour
{
    [SerializedDictionary("Towers", "Upgrades")] public SerializedDictionary<Tower, List<Stats>> towerUpgrades;

    public void UpgradeTower(Tower tower)
    {
        Stats upgradeStat = GetUpgradeForTower(tower);

        tower.UpgradeStats(upgradeStat);
        ParticleManager.Instance.CallPartical("Upgrade", tower.transform);
        tower.SetTowerStats();
    }

    public Stats GetUpgradeForTower(Tower tower, int plusLevel = 1)
    {
        foreach (var towerUpgrade in towerUpgrades)
        {
            if (towerUpgrade.Key.Index == tower.Index)
            {
                int nextLevel = tower.Level + plusLevel;
                foreach (var upgrade in towerUpgrade.Value)
                {
                    if (upgrade.Level == nextLevel)
                    {
                        return upgrade;
                    }
                }
            }
        }

        Debug.LogError("Нет такой башни");
        return null;
    }
}
