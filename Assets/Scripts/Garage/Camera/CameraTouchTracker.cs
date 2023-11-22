using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraTouchTracker : MonoBehaviour
{
    public event UnityAction<bool> PressedClick;
    
    private void OnMouseDown()
    {
        PressedClick?.Invoke(true);
    }

    private void OnMouseUp()
    {
        PressedClick?.Invoke(false);
    }
}
