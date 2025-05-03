using UnityEngine;

public class Placement : MonoBehaviour
{
    private PlacementUI placementUI;
    public PlacementType placementType;

    private Tower placedTower;
    public bool isTowerPlaced { get; private set; } = false;
    public bool isTowerUpgraded { get; private set; } = false;

    private void Awake()
    {
        placementUI = GetComponent<PlacementUI>();
        placementUI.Initialize(this);
    }

    public void SetTower()
    {
        if (placementType == PlacementType.AccommodationNoteble && BuildManager.Instance.TowerToBuild.GetType() == typeof(AccommodationTower))
        {
            Debug.Log("Здании Размещении нельзя сюда строить!");
        }

        else if (placedTower == null && BuildManager.Instance.TowerToBuild != null)
        {
            Tower buildedTower = Instantiate(BuildManager.Instance.TowerToBuild, transform.position, Quaternion.identity);
            buildedTower.Initialize();
            SoundSystem.Instance.Sound("Placed").Play();
            ParticleManager.Instance.CallPartical("Build", transform);

            placedTower = buildedTower;
            Inventory.Instance.SpendGold(buildedTower.towerCost);
            BuildManager.Instance.TowerToBuild = null;

            placementUI.Deprepare();
            isTowerPlaced = true;
            // Debug.Log("Башня поставлен");
        }
    }

    public Tower GetTower()
    {
        if (placedTower != null)
        {
            return placedTower;
        }
        else
        {
            Debug.LogWarning("Тут нет башни");
            return null;
        }
    }

    public void TowerUpgrade()
    {
        if (isTowerPlaced && !isTowerUpgraded)
        {
            BuildManager.Instance.UpgradeTower(placedTower);
            isTowerUpgraded = true;
        }
    }
}

/// <summary>
/// Типы клеток для размещении
/// </summary>
public enum PlacementType
{
    AccommodationEble,
    AccommodationNoteble
}