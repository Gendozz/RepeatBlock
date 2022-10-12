using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class PlayerInputHandler : ITickable
{
    readonly UserInputState _inputState;

    private Queue<DirectionToMove> _inputDirections = new Queue<DirectionToMove>();

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

        if(_inputState.IsMovingRight) _inputDirections.Enqueue(DirectionToMove.Right);
        if(_inputState.IsMovingLeft) _inputDirections.Enqueue(DirectionToMove.Left);
    }

    public DirectionToMove GetNextDirection()
    {
        return _inputDirections.Dequeue();
    }
}

public enum DirectionToMove
{
    Left,
    Right
}
