using UnityEngine;
using UnityEngine.UI;

// This script will tell you which direction you swiped in
public class SwipeManager : MonoBehaviour
{
	//[Tooltip("The text element we will display the swipe information in")]
	//public Text InfoText;

    private Lean.LeanFinger swipingFinger;

    protected virtual void OnEnable()
    {
        // Hook into the OnFingerDown event
        Lean.LeanTouch.OnFingerDown += OnFingerDown;

        // Hook into the OnFingerSet event
        Lean.LeanTouch.OnFingerSet += OnFingerSet;

        // Hook into the OnFingerUp event
        Lean.LeanTouch.OnFingerUp += OnFingerUp;
    }

    protected virtual void OnDisable()
    {
        // Unhook from the OnFingerDown event
        Lean.LeanTouch.OnFingerDown -= OnFingerDown;

        // Unhook from the OnFingerUp event
        Lean.LeanTouch.OnFingerSet -= OnFingerSet;

        // Unhook from the OnFingerUp event
        Lean.LeanTouch.OnFingerUp -= OnFingerUp;
    }

    public void OnFingerSet(Lean.LeanFinger finger)
    {
        // Is this the current finger?
        if (finger == swipingFinger)
        {
            // The scaled delta position magnitude required to register a swipe
            var swipeThreshold = Lean.LeanTouch.Instance.SwipeThreshold;

            // The amount of seconds we consider valid for a swipe
            var tapThreshold = Lean.LeanTouch.Instance.TapThreshold;

            // Get the scaled delta position between now, and 'swipeThreshold' seconds ago
            var recentDelta = finger.GetScaledSnapshotDelta(tapThreshold);

            // Has the finger recently swiped?
            if (recentDelta.magnitude > swipeThreshold)
            {
                // Store the swipe delta in a temp variable
                var swipe = finger.SwipeDelta;
                var left = new Vector2(-1.0f, 0.0f);
                var right = new Vector2(1.0f, 0.0f);
                var down = new Vector2(0.0f, -1.0f);
                var up = new Vector2(0.0f, 1.0f);

                if (SwipedInThisDirection(swipe, left + up) == true)
                {
                    //InfoText.text = "You swiped left and up!";
                    GridData.gridManager.TryMovePlayer(MoveDirection.LeftUp);
                }

                else if (SwipedInThisDirection(swipe, left + down) == true)
                {
                    //InfoText.text = "You swiped left and down!";
                    GridData.gridManager.TryMovePlayer(MoveDirection.LeftDown);
                }

                else if (SwipedInThisDirection(swipe, right + up) == true)
                {
                    //InfoText.text = "You swiped right and up!";
                    GridData.gridManager.TryMovePlayer(MoveDirection.RightUp);
                }

                else if (SwipedInThisDirection(swipe, right + down) == true)
                {
                    //InfoText.text = "You swiped right and down!";
                    GridData.gridManager.TryMovePlayer(MoveDirection.RightDown);
                }

                // Unset the finger so we don't continually add forces to it
                swipingFinger = null;
            }
        }
    }

    private bool SwipedInThisDirection(Vector2 swipe, Vector2 direction)
	{
		// Find the normalized dot product between the swipe and our desired angle (this will return the acos between the vectors)
		var dot = Vector2.Dot(swipe.normalized, direction.normalized);

		// With 8 directions, each direction takes up 45 degrees (360/8), but we're comparing against dot product, so we need to halve it
        ////changed this to 45 because we only want diagonal 
		var limit = Mathf.Cos(45f * Mathf.Deg2Rad);

		// Return true if this swipe is within the limit of this direction
		return dot >= limit;
	}

    public void OnFingerDown(Lean.LeanFinger finger)
    {
        // Set the current finger to this one
        swipingFinger = finger;
    }

    public void OnFingerUp(Lean.LeanFinger finger)
    {
        // Was the current finger lifted from the screen?
        if (finger == swipingFinger)
        {
            // Unset the current finger
            swipingFinger = null;
        }
    }
}