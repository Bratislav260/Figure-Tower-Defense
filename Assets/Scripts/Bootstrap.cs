using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] UnitController unitController;
    [SerializeField] TowerController towerController;
    [SerializeField] UnitSpawner unitSpawner;
    [SerializeField] InputHandler inputHandler;
    [SerializeField] BuildManager buildManager;
    [SerializeField] Inventory inventory;

    [SerializeField] UIController UIController;
    [SerializeField] CardBoard cardBoard;
    [SerializeField] AimController aimController;

    [SerializeField] ParticleManager particleManager;
    [SerializeField] SoundSystem soundSystem;

    private void Awake()
    {
        gameManager.Initialize();
        unitController.Initialize();
        unitSpawner.Initialize();
        towerController.Initialize();
        inventory.Initialize();

        buildManager.Initialize();
        inputHandler.Initialize();

        UIController.Initialize();
        cardBoard.Initialize();
        aimController.Initialize();

        particleManager.Initialize();
        soundSystem.Initialize();

        SoundSystem.Instance.Music("BackgroundMusic").Play();
    }

    private void Update()
    {
        unitController.UnitsUpdate();
        towerController.TowersUpdate();
    }

    private void FixedUpdate()
    {
        unitController.UnitsFixedUpdate();
    }
}
