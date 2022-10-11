using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class PlayerInputHandler : ITickable
{
    readonly UserInputState _inputState;

    private Queue<InputDirection> _inputDirections = new Queue<InputDirection>();

    public bool _HasNextinputDirections
    {
        get { return _inputDirections.Count > 0; }
    }

    public PlayerInputHandler(UserInputState inputState)
    {
        _inputState = inputState;
    }

    public void Tick()
    {
        _inputState.IsMovingLeft = Input.GetKeyDown(KeyCode.A);
        _inputState.IsMovingRight = Input.GetKeyDown(KeyCode.D);

        if(_inputState.IsMovingRight) _inputDirections.Enqueue(InputDirection.Right);
        if(_inputState.IsMovingLeft) _inputDirections.Enqueue(InputDirection.Left);
    }

    public InputDirection GetNextDirection()
    {
        return _inputDirections.Dequeue();
    }
}

public enum InputDirection
{
    Left,
    Right
}
