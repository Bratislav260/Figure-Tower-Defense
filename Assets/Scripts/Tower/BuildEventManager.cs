using UnityEngine;
using UnityEngine.Events;

public class BuildEventManager : MonoBehaviour
{
    public static UnityEvent onSetTowerBuild = new UnityEvent();
    public static void TowerToBuildSetted()
    {
        onSetTowerBuild.Invoke();
    }

    public static UnityEvent onUpgradeTower = new UnityEvent();
    public static void TowerUpgrade()
    {
        onUpgradeTower.Invoke();
    }
}
