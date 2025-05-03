using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject victoryScreen;
    [field: SerializeField] public List<Transform> pathPoints;

    public void Initialize()
    {
        Instance = this;
        Time.timeScale = 1;
        EventManager.onGameOver.AddListener(GameOver);
        EventManager.onGameWin.AddListener(Victory);

        gameOverScreen.SetActive(false);
        victoryScreen.SetActive(false);
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        EventManager.StopWave();
        EventManager.StopWaveMove();
        gameOverScreen.SetActive(true);
    }

    private void Victory()
    {
        victoryScreen.SetActive(true);
    }
}
