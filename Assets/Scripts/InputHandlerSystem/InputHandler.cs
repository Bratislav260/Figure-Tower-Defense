using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }
    private InputControls inputControls;
    private MouseClick mouseClick;
    private bool isReady = false;

    private InputAction leftClickAction;

    public void Initialize()
    {
        Instance = this;
        inputControls = new InputControls();
        mouseClick = GetComponent<MouseClick>();

        leftClickAction = inputControls.Player.LeftMouseClick;

        isReady = true;
        OnEnable();
    }

    #region - Enable / Disable -
    private void OnEnable()
    {
        if (!isReady)
            return;

        inputControls.Player.Enable();
        Register();
    }
    private void OnDisable()
    {
        inputControls.Disable();
        Unregister();
    }
    #endregion

    private void Register()
    {
        leftClickAction.performed += context => mouseClick.LeftButtonClick();
    }

    private void Unregister()
    {
        leftClickAction.performed -= context => mouseClick.LeftButtonClick();
    }
}