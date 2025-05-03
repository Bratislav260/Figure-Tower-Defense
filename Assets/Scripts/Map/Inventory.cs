using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    [field: SerializeField] public float goldCoffers { get; private set; }
    [field: SerializeField] public float upgradesCount { get; private set; }

    public void Initialize()
    {
        Instance = this;
    }

    public void GetGold(float gold)
    {
        goldCoffers += gold;
        UIEventManager.GoldUIUpdate();
    }

    public void SpendGold(float gold)
    {
        goldCoffers -= gold;
        UIEventManager.GoldUIUpdate();
    }

    public void GetUpgrade()
    {
        upgradesCount += 1;
        SoundSystem.Instance.Sound("UpgradeGet").Play();
        UIEventManager.UpgradesUIUpdate();
    }

    public void SpendUpgrade()
    {
        upgradesCount -= 1;
        UIEventManager.UpgradesUIUpdate();
    }
}
