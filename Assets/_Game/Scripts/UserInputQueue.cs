using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UserInputQueue
{
    private Queue<DirectionToMove> _inputDirections = new Queue<DirectionToMove>();

    public bool HasNextInputDirections => _inputDirections.Count > 0;

    public void EnqueueInput(DirectionToMove directionToMove)
    {
        _inputDirections.Enqueue(directionToMove);
    }

    public DirectionToMove GetNextDirection()
    {
        return _inputDirections.Dequeue();
    }
}