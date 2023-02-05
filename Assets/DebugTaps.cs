using UnityEngine;

public class DebugTaps : MonoBehaviour
{
    // Tap
    public void OnTapGesture()
    {
        Debug.Log("Tap!");
    }

    public void OnDoubleTapGesture()
    {
        Debug.Log("Double Tap!");
    }
    
    // Hold
    public void OnHoldStartedGesture()
    {
        Debug.Log("Holding Started!");
    }
    
    public void OnHoldDeltaGesture(Vector2 amount)
    {
        Debug.Log("Holding Delta Of: " + amount);
    }

    public void OnHoldEndedGesture()
    {
        Debug.Log("Holding Finished!");
    }
    
    // Pinch
    public void OnPinchStartedGesture()
    {
        Debug.Log("Pinching Started!");
    }
    
    public void OnPinchDeltaGesture(float amount)
    {
        Debug.Log("Pinching Delta Of: " + amount);
    }
    
    public void OnPinchDistanceGesture(float amount)
    {
        Debug.Log("Pinching Distance Of: " + amount);
    }
    
    public void OnPinchEndedGesture()
    {
        Debug.Log("Pinching Finished!");
    }
}
