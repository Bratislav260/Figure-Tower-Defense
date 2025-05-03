using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

public class CardBoard : MonoBehaviour
{
    [SerializeField] private RectTransform Panel;
    public List<TowerCard> cards = new List<TowerCard>();

    [SerializedDictionary("Card", "Tower")] public SerializedDictionary<TowerCard, Tower> cardsTower;

    public void Initialize()
    {
        SetTowers();
    }

    private void SetCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (!cardsTower.ContainsKey(cards[i]))
            {
                cardsTower.Add(cards[i], null);
            }
        }
    }

    private void UpdateChildObjectsList()
    {
        foreach (RectTransform child in Panel)
        {
            if (child.TryGetComponent<TowerCard>(out var card))
            {
                if (!cards.Contains(card))
                {
                    cards.Add(card);
                }
            }
        }
    }

    private void SetTowers()
    {
        foreach (var card in cardsTower)
        {
            card.Key.SetTower(card.Value);
        }
    }

    /// <summary>
    /// Изменение значении в реальном времени
    /// </summary>
    private void OnValidate()
    {
        UpdateChildObjectsList();
        SetCards();
    }
}