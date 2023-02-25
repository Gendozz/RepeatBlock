using System;
using UnityEngine;

public class PositionDeterminant
{
    private Vector3 _previousPosition = Vector3.zero;

    public Vector3 GetNextPosition(Direction direction)
    {
        Vector3 newPosition;

        switch (direction)
        {
            case Direction.Left:
                newPosition = _previousPosition + Vector3.forward;
                break;
            case Direction.Right:
                newPosition = _previousPosition + Vector3.right;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        _previousPosition = newPosition;

        return newPosition;
    }       
    
    public Vector3 GetNextPositionFromPosition(Direction direction, Vector3 fromPosition)
    {
        Vector3 newPosition;

        switch (direction)
        {
            case Direction.Left:
                newPosition = fromPosition + Vector3.forward;
                break;
            case Direction.Right:
                newPosition = fromPosition + Vector3.right;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        _previousPosition = newPosition;

        return newPosition;
    }

    public void ClearPreviousPosition()
    {
        _previousPosition = Vector3.zero;
    }
}
