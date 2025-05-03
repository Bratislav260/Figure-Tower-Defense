using TMPro;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upgradeCount;

    public void UpgradeTower()
    {
        BuildManager.Instance.SetTowerToUpgrade();
    }

    public void Awake()
    {
        UIEventManager.onUpgradeUIUpdate.AddListener(UpgradeCountUpdate);
        UpgradeCountUpdate();
    }

    public void UpgradeCountUpdate()
    {
        upgradeCount.text = $"{Inventory.Instance.upgradesCount}";
    }
}
