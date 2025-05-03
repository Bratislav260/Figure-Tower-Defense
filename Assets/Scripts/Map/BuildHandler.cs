using UnityEngine;

public class BuildHandler
{
    public void Initialize()
    {
        BuildEventManager.onSetTowerBuild.AddListener(PlacementTowerSet);
        BuildEventManager.onUpgradeTower.AddListener(PlacementTowerUpgrade);
    }

    private void PlacementTowerSet()
    {
        BuildManager.Instance.placementAction = null;
        BuildManager.Instance.placementAction += SetTower;
    }

    private void PlacementTowerUpgrade()
    {
        BuildManager.Instance.placementAction = null;
        BuildManager.Instance.placementAction += TowerUpgrade;
    }

    public void SetTower(Placement placement)
    {
        placement.SetTower();
    }
    public void TowerUpgrade(Placement placement)
    {
        placement.TowerUpgrade();
    }
}
