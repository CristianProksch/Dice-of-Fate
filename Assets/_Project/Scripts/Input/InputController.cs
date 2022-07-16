using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
    private LayerMask _uiLayers;
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

    public static bool IsMouseOverUI()
    {
        return Instance.IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == _uiLayers)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
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
