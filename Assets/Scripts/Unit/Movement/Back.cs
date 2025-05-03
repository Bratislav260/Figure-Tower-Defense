using UnityEngine;

public class Back : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector2 baseSize;
    private Vector2 baseOffset;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        baseSize = boxCollider.size;
        baseOffset = boxCollider.offset;
    }

    public void DecreaseSize()
    {
        boxCollider.offset = new Vector2(-0.5f, 0f);
        boxCollider.size = new Vector2(0.5f, 0.2f);
    }

    public void SetBaseSize()
    {
        boxCollider.size = baseSize;
        boxCollider.offset = baseOffset;
    }
}
