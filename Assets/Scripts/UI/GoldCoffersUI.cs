using System;
using TMPro;
using UnityEngine;

public class GoldCoffers : MonoBehaviour
{
    public TextMeshProUGUI goldCount;

    public void Initialize()
    {
        UIEventManager.onGoldUIUpdate.AddListener(GoldUIUpdate);
        GoldUIUpdate();
    }

    public void GoldUIUpdate()
    {
        goldCount.text = Convert.ToString(Inventory.Instance.goldCoffers);
    }
}
