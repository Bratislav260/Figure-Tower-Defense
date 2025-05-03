using System;
using UnityEngine;

/// <summary>
/// Класс отвечающий за строительство на карте
/// </summary>
public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    private BuildHandler buildHandler = new BuildHandler();

    private TowerUpgradeController upgradeTower;

    public Action<Placement> placementAction;
    public Tower TowerToBuild;

    public void Initialize()
    {
        Instance = this;
        upgradeTower = GetComponent<TowerUpgradeController>();
        buildHandler.Initialize();
    }

    public void SetTowerToBuild(Tower newTower)
    {
        if (Inventory.Instance.goldCoffers >= newTower.stats.GetStat(Stat.cost))
        {
            TowerToBuild = newTower;
            BuildEventManager.TowerToBuildSetted();
        }
    }

    public void SetTowerToUpgrade()
    {
        if (Inventory.Instance.upgradesCount > 0)
        {
            BuildEventManager.TowerUpgrade();
        }
    }

    public void UpgradeTower(Tower tower)
    {
        Inventory.Instance.SpendUpgrade();
        upgradeTower.UpgradeTower(tower);
        SoundSystem.Instance.Sound("TowerUpgraded").Play();
    }
}
