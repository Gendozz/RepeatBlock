using UnityEngine;
using Zenject;

public class PlayerInputHandler : ITickable
{
    private readonly UserInputState _inputState;

    private readonly UserInputQueue _userInputQueue;

    public PlayerInputHandler(UserInputState inputState, UserInputQueue userInputQueue)
    {
        _inputState = inputState;
        _userInputQueue = userInputQueue;
    }

    public void Tick()
    {
        _inputState.IsMovingLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        _inputState.IsMovingRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        if(_inputState.IsMovingRight) _userInputQueue.EnqueueInput(DirectionToMove.Right);
        if (_inputState.IsMovingLeft)
        {
            _userInputQueue.EnqueueInput(DirectionToMove.Left);
        }
    }
}

public enum DirectionToMove
{
    Left,
    Right
}
