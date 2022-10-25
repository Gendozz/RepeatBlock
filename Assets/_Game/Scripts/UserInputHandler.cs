using UnityEngine;
using Zenject;

public class UserInputHandler : IInitializable, ITickable
{
    private readonly UserInputState _inputState;

    private readonly UserInputQueue _userInputQueue;

    private readonly SignalBus _signalBus;

    private bool _shouldDetectInput = true;

    private int _currentInputLimit;

    public UserInputHandler(UserInputState inputState, UserInputQueue userInputQueue, SignalBus signalBus)
    {
        _inputState = inputState;
        _userInputQueue = userInputQueue;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerFinishedPath>(() => _shouldDetectInput = false);
        _signalBus.Subscribe<AllBlocksMoved>(() => _shouldDetectInput = true);
        _signalBus.Subscribe((PathGenerationCompleted args) => _currentInputLimit = args.WaypointsAmount); // MISTAKE IF CHANGE RIGHTWAY CHECKER
    }

    public void Tick()
    {
        if (!_shouldDetectInput)
            return;

        if(_currentInputLimit <= 0)
            return;
        
        _inputState.IsMovingLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        _inputState.IsMovingRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        if (_inputState.IsMovingRight)
        {
            _userInputQueue.EnqueueInput(DirectionToMove.Right);
            _currentInputLimit--;
        }

        if (_inputState.IsMovingLeft)
        {
            _userInputQueue.EnqueueInput(DirectionToMove.Left);
            _currentInputLimit--;
        }
    }
}


public enum DirectionToMove
{
    Left,
    Right
}