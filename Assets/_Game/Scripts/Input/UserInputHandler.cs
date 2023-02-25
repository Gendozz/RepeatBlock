using UnityEngine;
using Zenject;

public class UserInputHandler : ITickable, IAbleToPause
{
    private readonly UserInputState _inputState;

    private readonly DirectionsQueue _userInputQueue;

    private bool _shouldDetectInput = false;

    private int _currentInputLimit = 0;

    // Touch
    private float _xPosOnSwipeStart1;

    public UserInputHandler(
        UserInputState inputState,
        DirectionsQueue userInputQueue)
    {
        _inputState = inputState;
        _userInputQueue = userInputQueue;
    }

    public void Tick()
    {
        if (!_shouldDetectInput)
            return;

        if (_currentInputLimit <= 0)
            return;

        EnqueueUserInput();
    }

    private void EnqueueUserInput()
    {
        ListenForKeyboardInput();
        ListenForTouchInput();

        if (_inputState.IsMovingRight)
        {
            _userInputQueue.EnqueueDirection(Direction.Right);
            _currentInputLimit--;
        }

        if (_inputState.IsMovingLeft)
        {
            _userInputQueue.EnqueueDirection(Direction.Left);
            _currentInputLimit--;
        }
    }

    private void ListenForKeyboardInput()
    {
        _inputState.IsMovingLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        _inputState.IsMovingRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
    }

    private void ListenForTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase.Equals(TouchPhase.Began))
            {
                _xPosOnSwipeStart1 = touch.position.x;
            }

            if (touch.phase.Equals(TouchPhase.Ended) || touch.phase.Equals(TouchPhase.Canceled))
            {
                float xDist = Mathf.Abs(_xPosOnSwipeStart1 - touch.position.x);
                if (xDist > 100)
                {
                    if (_xPosOnSwipeStart1 < touch.position.x)
                    {
                        _inputState.IsMovingRight = true;
                        Debug.Log("Right swipe");
                    }
                    if(_xPosOnSwipeStart1 > touch.position.x)
                    {
                        _inputState.IsMovingLeft = true;
                        Debug.Log("Left swipe");
                    }
                }
            }
        }
    }

    public void SetShouldDetectInput(bool shouldDetectInput)
    {
        _shouldDetectInput = shouldDetectInput;
    }

    public void SetUserInputLimit(int inputLimit)
    {
        _currentInputLimit = inputLimit;
    }

    public void Pause()
    {
        _shouldDetectInput = false; // May cause bug!!!!
    }

    public void Unpause()
    {
        _shouldDetectInput = true;
    }
}

public enum Direction
{
    Left,
    Right
}