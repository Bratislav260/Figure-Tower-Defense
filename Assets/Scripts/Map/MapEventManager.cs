using UnityEngine;
using UnityEngine.Events;

public class MapEventManager
{
    public static UnityEvent onMouseEnter = new UnityEvent();
    public static UnityEvent onMouseExit = new UnityEvent();

    public static void OnMouseEnter()
    {
        onMouseEnter.Invoke();
    }

    public static void OnMouseExit()
    {
        onMouseExit.Invoke();
    }
}
