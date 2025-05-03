using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public static TowerController Instance { get; private set; }

    public List<Tower> allTowersList = new List<Tower>();

    public void Initialize()
    {
        Instance = this;
    }

    public void Remove(Tower tower)
    {
        allTowersList.Remove(tower);
    }

    public void TowersUpdate()
    {
        foreach (var tower in allTowersList)
        {
            tower.TowerUpdate();
        }
    }
}
