using System.Collections.Generic;

public class DirectionsQueue
{
    private Queue<Direction> _inputDirections = new Queue<Direction>();

    public bool HasNextInputDirections => _inputDirections.Count > 0;

    public void EnqueueDirection(Direction directionToMove)
    {
        _inputDirections.Enqueue(directionToMove);
    }

    public Direction GetNextDirection()
    {
        return _inputDirections.Dequeue();
    }
}