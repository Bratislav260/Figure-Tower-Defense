using System.Collections;
using UnityEngine;

public class AccommodationTower : Tower
{
    #region --- Подклассы ---

    [Header("Подклассы")]
    [SerializeField] private AccommodationObject objectAccommodation;
    #endregion

    #region --- UI ---
    [Header("UI")]
    public bool IsHasChildrenSprite;
    public bool IsHasChildUI;
    #endregion

    #region --- Параметры ---

    [Header("Параметры")]
    [SerializeField] private int objectCount;
    [SerializeField] private float setTime;
    [SerializeField] private float isFirstLaunchTime;
    private bool isFirstLaunch = true;
    private int currentObject = 0;
    private Transform UIAccommodationObject;
    #endregion

    public override void Initialize()
    {
        base.Initialize();

        spriteController.Initialize(child: objectAccommodation.transform);

        if (IsHasChildUI)
            UIAccommodationObject = transform.GetChild(0);
        StartObjectSet();
    }

    public void StartObjectSet()
    {
        StartCoroutine(CoroutineObjectSet());
    }

    private IEnumerator CoroutineObjectSet()
    {
        if (objectCount <= currentObject)
        {
            yield return new WaitForSeconds(0.5f);
            TowerController.Instance.Remove(this);
            Destroy(gameObject, 5f);
        }

        if (isFirstLaunch)
        {
            yield return new WaitForSeconds(isFirstLaunchTime);
            isFirstLaunch = false;
        }
        else
        {
            if (IsHasChildUI)
                UIAccommodationObject.gameObject.SetActive(true);

            yield return new WaitForSeconds(setTime);
        }

        SetObject();
        currentObject += 1;

        yield return null;
    }

    private void SetObject()
    {
        Vector2 setPosition = new Vector2(transform.position.x, transform.position.y + 2.5f); // 2.5 - столько надо, чтобы он ставился ровна
        // AccommodationObject setedBomb = Instantiate(objectAccommodation, position, Quaternion.identity);
        objectAccommodation.Set(setPosition);
        objectAccommodation.Accommodate(this);

        if (IsHasChildUI)
            UIAccommodationObject.gameObject.SetActive(false);
    }

    public override void SetTowerStats()
    {
        base.SetTowerStats();
        objectAccommodation.UpgradeStats(Level);
    }

    public override void SetNewSprite()
    {
        spriteController.SpriteChange(spriteRenderer, Level, IsChildrenSprite: IsHasChildrenSprite);
    }
}
