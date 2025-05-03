using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Событии Босса
/// </summary>
public class BossEventManager
{
    public static UnityEvent onStateFinish = new UnityEvent();

    public static void MoveStateAction()
    {
        onStateFinish.Invoke();
    }
}
