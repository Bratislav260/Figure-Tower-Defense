using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent onGameOver = new UnityEvent();
    public static void GameOver()
    {
        onGameOver.Invoke();
    }

    public static UnityEvent onGameWin = new UnityEvent();
    public static void GameWin()
    {
        onGameWin.Invoke();
    }

    public static UnityEvent onStopWave = new UnityEvent();
    public static void StopWave()
    {
        onStopWave.Invoke();
    }

    public static UnityEvent onStopWaveMove = new UnityEvent();
    public static void StopWaveMove()
    {
        onStopWaveMove.Invoke();
    }

    public static UnityEvent<int> onNewWaveStarts = new UnityEvent<int>();
    public static void NewWaveStarted(int waveCount)
    {
        onNewWaveStarts.Invoke(waveCount);
    }

    public static UnityEvent onLastWave = new UnityEvent();
    public static void LastWave()
    {
        onLastWave.Invoke();
    }
}
