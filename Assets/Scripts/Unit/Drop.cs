using UnityEngine;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

public class Drop : MonoBehaviour
{
    [SerializedDictionary("Gold", "Percentages")] public SerializedDictionary<int, float> goldPercentages;

    public void Droping()
    {
        GoldDrop();
        GetUpgrade();
    }

    private void GoldDrop()
    {
        Inventory.Instance.GetGold(CalculateDropGold());
    }

    private void GetUpgrade()
    {
        float chance = Random.Range(0, 101);

        if (chance > 90)
        {
            Inventory.Instance.GetUpgrade();
        }
    }

    private int CalculateDropGold()
    {
        List<int> possibleGold = new List<int>();
        int chance = Random.Range(0, 101);

        foreach (var goldChance in goldPercentages)
        {
            if (chance <= goldChance.Value)
            {
                possibleGold.Add(goldChance.Key);
            }
        }

        return possibleGold[Random.Range(0, possibleGold.Count)];
    }
}
