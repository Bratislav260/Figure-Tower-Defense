using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Placement placement;

    public void Initialize(Placement placement)
    {
        this.placement = placement;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Prepare();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deprepare();
    }

    public void Prepare()
    {
        if (!placement.isTowerPlaced)
        {
            AimController.Instance.SetAim(placement.transform);
            // spriteRenderer.color = Color.red;
        }
    }

    public void Deprepare()
    {
        if (!placement.isTowerPlaced)
        {
            AimController.Instance.RemoveAim();
            // spriteRenderer.color = baseColor;
        }
    }
}
