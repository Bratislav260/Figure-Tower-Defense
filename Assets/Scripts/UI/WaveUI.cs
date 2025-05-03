using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveCountText;
    private int waveCount = 0;

    public void Initialize()
    {
        UIEventManager.onWaveUIUpdate.AddListener(WaveUIUpdate);
        WaveUIUpdate(waveCount);
    }

    public void WaveUIUpdate(int waveCount)
    {
        this.waveCount = waveCount;
        waveCountText.text = $"{waveCount}";
    }
}
