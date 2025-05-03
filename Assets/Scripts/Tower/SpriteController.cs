using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private Sprite[] mainSprites;
    [SerializeField] private Sprite[] bulletSprites;
    [SerializeField] private Sprite[] childrenSprites;

    private Bullet bulletType;
    private Transform _child;
    private int Level;

    public void Initialize(Bullet bulletType = null, Transform child = null)
    {
        this.bulletType = bulletType;
        _child = child;
    }

    public void SpriteChange(SpriteRenderer spriteRenderer, int level,
                            bool IsBulletSprite = false,
                            bool IsChildrenSprite = false)
    {
        Level = level - 1;
        spriteRenderer.sprite = mainSprites[Level];

        if (IsBulletSprite)
        {
            BulletSpriteChange(bulletType.spriteRenderer);
        }
        if (IsChildrenSprite)
        {
            ChildrenSpriteChange(_child.GetComponent<SpriteRenderer>());
        }
    }

    public void BulletSpriteChange(SpriteRenderer bulletSpriteRenderer)
    {
        bulletSpriteRenderer.sprite = bulletSprites[Level];
    }

    public void ChildrenSpriteChange(SpriteRenderer childrenSpriteRenderer)
    {
        childrenSpriteRenderer.sprite = childrenSprites[Level];
    }
}
