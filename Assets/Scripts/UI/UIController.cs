using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GoldCoffers goldCoffers;
    [SerializeField] private WaveUI waveUI;

    public void Initialize()
    {
        goldCoffers.Initialize();
        waveUI.Initialize();
    }
}
