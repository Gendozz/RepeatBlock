using UnityEngine;
using Zenject;

public class UserInputHandler : IInitializable, ITickable
{
    private readonly UserInputState _inputState;

    private readonly UserInputQueue _userInputQueue;

    private readonly SignalBus _signalBus;

    private readonly PlayerMoveHandler _playerMoveHandler;

    private bool _shouldDetectInput = false;

    private int _currentInputLimit = 6; //TODO: Initialize in Initial actions

    public UserInputHandler(
        UserInputState inputState,
        UserInputQueue userInputQueue,
        SignalBus signalBus,
        PlayerMoveHandler playerMoveHandler)
    {
        _inputState = inputState;
        _userInputQueue = userInputQueue;
        _signalBus = signalBus;
        _playerMoveHandler = playerMoveHandler;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<InitialActionsDone>(() =>
        {
            _shouldDetectInput = true;
            Debug.Log($"User input now should detect input. _shouldDetectInput = {_shouldDetectInput}");

        });

        _signalBus.Subscribe<PlayerFinishedPath>(() =>
        {
            Debug.Log("User input now shouldn't detect input");
            _shouldDetectInput = false;
        });
        
        _signalBus.Subscribe<OppenentFinishedPath>(() =>
        {
            Debug.Log("User input now should detect input");
            _shouldDetectInput = true;
        });

        _signalBus.Subscribe((PathGenerationCompleted args) =>
        {
            _currentInputLimit = args.WaypointsAmount; // MISTAKE IF CHANGE RIGHTWAY CHECKER
            Debug.Log($"_currentInputLimit = {_currentInputLimit}");
        });
    }

    public void Tick()
    {
        if (!_shouldDetectInput)
            return;

        if (_currentInputLimit <= 0)
            return;

        EnqueueUserInput();

        if (_userInputQueue.HasNextInputDirections)
        {
            ApplyUserInput();
        }
    }

    private void EnqueueUserInput()
    {
        _inputState.IsMovingLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        _inputState.IsMovingRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        if (_inputState.IsMovingRight)
        {
            _userInputQueue.EnqueueInput(DirectionToMove.Right);
            _currentInputLimit--;
            Debug.Log("Enqueued user input - Right");
        }

        if (_inputState.IsMovingLeft)
        {
            _userInputQueue.EnqueueInput(DirectionToMove.Left);
            _currentInputLimit--;
            Debug.Log("Enqueued user input - Left");
        }
    }

    private void ApplyUserInput()
    {
        //Debug.Log("Try to handle move");
        if (_playerMoveHandler.CanHandleMove)
        {
            Debug.Log("Applyied user input");
            _playerMoveHandler.HandleMove(_userInputQueue.GetNextDirection());
        }
    }
}


public enum DirectionToMove
{
    Left,
    Right
}