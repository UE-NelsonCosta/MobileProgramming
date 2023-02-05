using UnityEngine;
using UnityEngine.Events;

// Helper Classes For Somple UI Cleanup
[System.Serializable]
public class TapGestureEvents
{
    public UnityEvent OnTapGestureEvent;
    public UnityEvent OnDoubleTapGestureEvent;
}

[System.Serializable]
public class HoldGestureEvents
{
    public UnityEvent OnHoldStartedEvent;
    public UnityEvent<Vector2> OnHoldEvent;
    public UnityEvent OnHoldEndedEvent;
}

[System.Serializable]
public class PinchGestureEvents
{
    public UnityEvent OnPinchGestureStartedEvent;
    public UnityEvent<float> OnPinchDeltaGestureEvent;
    public UnityEvent<float> OnPinchDistanceGestureEvent;
    public UnityEvent OnPinchGestureEndedEvent;
}

public class InputProcessor : MonoBehaviour
{
    // Exposed Variables
    [field: SerializeField] public TapGestureEvents TapGestureEvents { get; set; }
    
    [field: SerializeField] public HoldGestureEvents HoldGestureEvents { get; set; }
    [field: SerializeField] public PinchGestureEvents PinchGestureEvents { get; set; }

    // Basic Timer Variable So We Can Control When Something Is Considered A Tap VS A Hold
    [SerializeField] private float TapToHoldThreshold = 0.15f;
    [SerializeField] private float DoubleTapTimer = 0.2f;
    
    // Internal Variables
    // Pinch Management
    private Vector2 PrimaryPinchPosition = Vector2.zero;
    private Vector2 SecondaryPinchPosition = Vector2.zero;
    
    // Hold Management
    private Vector2 HoldLocation = Vector2.zero;
    private float HoldTimer = 0.0f;

    // Tapping Management
    private float TapTimer = 0.0f;
    private float TimeBetweenTaps = 0.0f;
    private int TapCounter = 0;
    
    private void Update()
    {
        Touch[] touches = Input.touches;

        // Process Continuous Gestures!
        ProcessContinuousGestures(touches);
        
        // Process Event Driven Gestures
        ProcessEventGestures(touches);
    }

    #region ContinuousGestures

    private void ProcessContinuousGestures(Touch[] touches)
    {
        // Let's Try Do A Pinch Gesture!
        if (touches.Length >= 2)
        {
            ProcessPinchGesture(touches);
            return;
        }
        
        // If We Don't Pinch, Then Let's Set Them Back To Their Normal State
        ResetPinchTrackingVariables();

        if (touches.Length == 1)
        {
            HoldTimer += Time.deltaTime;
            if (HoldTimer < TapToHoldThreshold)
            {
                return;
            }

            
            ProcessHoldGesture(touches[0]);
            return;
        }
        
        // If We Don't Pinch, Then Let's Set Them Back To Their Normal State
        ResetHoldTrackingVariables();
    }

    private void ProcessPinchGesture(Touch[] touches)
    {
        // Just Starting The Pinch So Let's Just Keep Track Of The Initial Positions
        if (PrimaryPinchPosition == Vector2.zero || SecondaryPinchPosition == Vector2.zero)
        {
            // Starting To Pinch!
            PrimaryPinchPosition = touches[0].position;
            SecondaryPinchPosition = touches[1].position;
            
            OnPinchGestureStarted();
            
            return;
        }
        
        // Note: This is similar to using Vector2.Distance()
        float previousFrameDistance = (SecondaryPinchPosition - PrimaryPinchPosition).magnitude;
        
        // Else Find The Delta And Call The Event!
        PrimaryPinchPosition = touches[0].position;
        SecondaryPinchPosition = touches[1].position;

        // Note: This is similar to using Vector2.Distance()
        float newFrameDistance = (SecondaryPinchPosition - PrimaryPinchPosition).magnitude;

        // Delta Calculation
        float pinchDelta = newFrameDistance - previousFrameDistance;
        
        // Let the world Know!
        OnPinchDeltaGesture(pinchDelta);
        OnPinchDistanceGesture(newFrameDistance);
    }
    
    private void ProcessHoldGesture(Touch touch)
    {
        // Just Starting The Pinch So Let's Just Keep Track Of The Initial Positions
        if (HoldLocation == Vector2.zero)
        {
            // Starting To Pinch!
            HoldLocation = touch.position;
            
            OnHoldGestureStarted();
            
            return;
        }

        // Delta Calculation
        Vector2 holdDelta = touch.position - HoldLocation;
        
        HoldLocation = touch.position;

        // Let the world Know!
        OnHoldDeltaGesture(holdDelta);
    }
    
    #endregion

    #region EventGestures

    // TODO: What about double taps that aren't in the same area? :thinking:
    // TODO: There is a bug still where the ended touch can still be considered a touch! D:<
    private void ProcessEventGestures(Touch[] touches)
    {
        // Let's Find A Tap! (First Touch Only) (No Multitouch Shenanigans)
        if (touches.Length == 1)
        {
            Touch touch = touches[0];

            // If The Timer Is Too High Then It's A Hold Not A Tap
            if (TapTimer > TapToHoldThreshold)
            {
                ResetTapTrackingVariables();
                return;
            }

            // So We have a tap! Let's Time Between The Start And End And Ensure It's A Tap And Not A Hold
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                // Tick The Timer
                TapTimer += Time.deltaTime;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (TapTimer < TapToHoldThreshold)
                {
                    // Valid Touch! Nice! Reset The Timer!
                    ++TapCounter;
                    TapTimer = 0;   
                }
            }
        }
        
        if (TapCounter > 0)
        {
            TimeBetweenTaps += Time.deltaTime;
            if (TimeBetweenTaps > DoubleTapTimer || TapCounter == 2)
            {
                if (TapCounter == 1)
                {
                    OnTapGesture();
                }

                if (TapCounter == 2)
                {
                    OnDoubleTapGesture();
                }
                
                ResetTapTrackingVariables();
                TapCounter = 0;
            }
        }
    }

    #endregion
    
    #region EventsDispatching

    private void OnTapGesture()
    {
        TapGestureEvents.OnTapGestureEvent.Invoke();
    }

    private void OnDoubleTapGesture()
    {
        TapGestureEvents.OnDoubleTapGestureEvent.Invoke();
    }

    private void OnHoldGestureStarted()
    {
        HoldGestureEvents.OnHoldStartedEvent.Invoke();
    }

    private void OnHoldDeltaGesture(Vector2 amount)
    {
        HoldGestureEvents.OnHoldEvent.Invoke(amount);
    }

    private void OnHoldGestureEnded()
    {
        HoldGestureEvents.OnHoldEndedEvent.Invoke();
    }
        
        
    private void OnPinchGestureStarted()
    {
        PinchGestureEvents.OnPinchGestureStartedEvent.Invoke();
    }

    private void OnPinchDeltaGesture(float amount)
    {
        PinchGestureEvents.OnPinchDeltaGestureEvent.Invoke(amount);
    }
    
    private void OnPinchDistanceGesture(float amount)
    {
        PinchGestureEvents.OnPinchDistanceGestureEvent.Invoke(amount);
    }
    
    private void OnPinchGestureEnded()
    {
        PinchGestureEvents.OnPinchGestureEndedEvent.Invoke();
    }

    #endregion

    #region ResetFuncationality

    private void ResetTapTrackingVariables()
    {
        TapTimer = 0.0f;
        TimeBetweenTaps = 0.0f;
    }

    private void ResetPinchTrackingVariables()
    {
        if (PrimaryPinchPosition != Vector2.zero || SecondaryPinchPosition != Vector2.zero)
        {
            PrimaryPinchPosition = Vector2.zero;
            SecondaryPinchPosition = Vector2.zero;

            OnPinchGestureEnded();
        }
    }

    private void ResetHoldTrackingVariables()
    {
        if (HoldLocation != Vector2.zero)
        {
            HoldLocation = Vector2.zero;

            OnHoldGestureEnded();
        }

        HoldTimer = 0;
    }


    #endregion
}
