using UnityEngine;

public class PositionsProvider
{
    private PositionDeterminant _positionsDeterminant;

    private PositionsHolder _positionsHolder;

    public PositionsProvider()
    {
        _positionsDeterminant = new PositionDeterminant();
        _positionsHolder = new PositionsHolder();
    }

    public Vector3 GetNewPosition(Direction direction)
    {
        Vector3 newPosition = _positionsDeterminant.GetNextPosition(direction);
        _positionsHolder.EnqueuePosition(newPosition);
        return newPosition;
    }
    
    public Vector3 GetNewPositionFromPosition(Direction direction, Vector3 fromPosition)
    {
        Vector3 newPosition = _positionsDeterminant.GetNextPositionFromPosition(direction, fromPosition);
        _positionsHolder.EnqueuePosition(newPosition);
        return newPosition;
    }

    public Vector3 GetPosition(bool leaveSaved)
    {
        return leaveSaved ? _positionsHolder.PeekPosition() : _positionsHolder.DequeuePosition();
    }

    public int GetPositionCount()
    {
        return _positionsHolder.PositionsCount();
    }

    public void ClearPositions()
    {
        _positionsHolder.ClearPositions();
    }
}
