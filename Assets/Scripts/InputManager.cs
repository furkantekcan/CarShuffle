using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    private PlayerControls playerContros;
    private Camera mainCamera;

    #region Unity Callbacks

    private void Awake()
    {
        playerContros = new PlayerControls();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        playerContros.Enable();
    }

    private void OnDisable()
    {
        playerContros.Disable();
    }

    private void Start()
    {
        playerContros.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerContros.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    #endregion

    void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, playerContros.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);

    }
    void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, playerContros.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);

    }
}
