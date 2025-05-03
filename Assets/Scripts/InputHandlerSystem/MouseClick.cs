using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public float rayDistance = 10f;
    [SerializeField] private LayerMask layers;

    private Vector2 GetMousePosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(mousePosition);

        return rayOrigin;
    }

    public void LeftButtonClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetMousePosition(), Vector2.right, rayDistance, layers);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<Placement>(out var placement))
            {
                BuildManager.Instance.placementAction?.Invoke(placement);
                BuildManager.Instance.placementAction = null;
            }
        }

        // Debug.Log("Left Button Clicked");
    }
}
