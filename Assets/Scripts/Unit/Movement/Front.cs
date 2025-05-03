using UnityEngine;

public class Front : MonoBehaviour
{
    private UnitMovement unitMovement;
    private BoxCollider2D boxCollider;
    public int ColliderInCount = 0;
    private Vector2 baseSize;
    private Vector2 baseOffset;
    private UnitMovement otherObj;

    private void Awake()
    {
        unitMovement = GetComponentInParent<UnitMovement>();
        boxCollider = GetComponentInParent<BoxCollider2D>();
        baseSize = boxCollider.size;
        baseOffset = boxCollider.offset;
    }

    public void DecreaseSize()
    {
        boxCollider.size = new Vector2(0.2f, 0.3f);
        boxCollider.offset = new Vector2(0.2f, 0f);
    }

    public void SetBaseSize()
    {
        boxCollider.size = baseSize;
        boxCollider.offset = baseOffset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Back"))
        {
            ColliderInCount++;
            unitMovement.StopMove();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Back"))
        {
            ColliderInCount--;

            if (ColliderInCount <= 0)
            {
                unitMovement.StartMove();
            }
        }
    }
}
