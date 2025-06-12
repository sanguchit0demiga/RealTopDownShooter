using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CrosshairFollow : MonoBehaviour
{
    private RectTransform crosshairRect;

    private void Awake()
    {
        crosshairRect = GetComponent<RectTransform>();
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        crosshairRect.position = mousePos;
    }
}
