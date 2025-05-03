using UnityEngine;
using UnityEngine.Events;

public class UIEventManager
{
    public static UnityEvent onGoldUIUpdate = new UnityEvent();
    public static void GoldUIUpdate()
    {
        onGoldUIUpdate.Invoke();
    }

    public static UnityEvent<int> onWaveUIUpdate = new UnityEvent<int>();
    public static void WaveUIUpdate(int wave)
    {
        onWaveUIUpdate.Invoke(wave);
    }

    public static UnityEvent onUpgradeUIUpdate = new UnityEvent();
    public static void UpgradesUIUpdate()
    {
        onUpgradeUIUpdate.Invoke();
    }
}
