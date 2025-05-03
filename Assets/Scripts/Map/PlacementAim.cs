using UnityEngine;

public class AimController : MonoBehaviour
{
    public static AimController Instance;
    public GameObject Aim;

    public void Initialize()
    {
        Instance = this;
    }

    public void SetAim(Transform placement)
    {
        Aim.transform.position = placement.position;
        Aim.SetActive(true);
    }

    public void RemoveAim()
    {
        Aim.SetActive(false);
    }
}
