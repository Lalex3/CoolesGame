using UnityEngine;
using Lean.Touch;

public class TouchDebug : MonoBehaviour
{
    void Update()
    {
        var fingers = LeanTouch.GetFingers(false, false);
        if (fingers.Count > 0)
            Debug.Log("Fingers: " + fingers.Count + "  Position: " + fingers[0].ScreenPosition);
    }
}
