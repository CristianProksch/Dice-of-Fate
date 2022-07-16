using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputController : MonoBehaviour
{
    #region Singleton
    public static InputController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region Inspector
    [SerializeField]
    private UnityEvent _onMouseDown;
    [SerializeField]
    private UnityEvent _onMouseUp;
    #endregion

    public void OnClick(CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                _onMouseDown?.Invoke();
                break;
            case InputActionPhase.Performed:
                _onMouseUp?.Invoke();
                break;
        }
    }

    public static void AddMouseDownListener(UnityAction listener)
    {
        Instance._onMouseDown.AddListener(listener);
    }

    public static void RemoveMouseDownListener(UnityAction listener)
    {
        Instance._onMouseDown.RemoveListener(listener);
    }

    public static void AddMouseUpListener(UnityAction listener)
    {
        Instance._onMouseUp.AddListener(listener);
    }

    public static void RemoveMouseUpListener(UnityAction listener)
    {
        Instance._onMouseUp.RemoveListener(listener);
    }

    /// <summary>
    /// Returns the world position of the mouse
    /// </summary>
    public static Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
