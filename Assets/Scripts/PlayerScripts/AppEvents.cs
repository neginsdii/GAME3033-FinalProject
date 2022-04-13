using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents : MonoBehaviour
{
    public delegate void OnMouseCursorEnable(bool enabled);

    public static event OnMouseCursorEnable MouseCursorEnabled;


    public static void InvokeMouseCursorEnable(bool enabled)
    {
        MouseCursorEnabled?.Invoke(enabled);
    }
}
