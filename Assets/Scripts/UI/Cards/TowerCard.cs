using TMPro;
using UnityEngine;

public class TowerCard : MonoBehaviour
{
    private Tower cardTower;
    [SerializeField] private TextMeshProUGUI cardCost;

    public void SetTowerToBuild()
    {
        BuildManager.Instance.SetTowerToBuild(cardTower);
    }
    public void SetTower(Tower tower)
    {
        cardTower = tower;
        cardCost.text = $"{cardTower.stats.GetStat(Stat.cost)}";
    }
}
