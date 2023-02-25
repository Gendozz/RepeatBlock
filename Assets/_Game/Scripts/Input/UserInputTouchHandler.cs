using UnityEngine;

public class UserInputTouchHandler 
{

    private readonly UserInputState _inputState;

    private readonly DirectionsQueue _userInputQueue;

    private bool _shouldDetectInput = false;

    private int _currentInputLimit = 0;

    public UserInputTouchHandler(
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
        _inputState.IsMovingLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        _inputState.IsMovingRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

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

}
