using System.Collections.Generic;

public class DirectionsHolder
{
    private Queue<Direction> _directions = new Queue<Direction>();

    public void EnqueueDirection(Direction direction)
    {
        _directions.Enqueue(direction);
    }

    public Direction DequeueDirection()
    {
        return _directions.Dequeue();
    }

    public Direction PeekDirection()
    {
        return _directions.Peek();
    }

    public int DirectionsCount() { return _directions.Count; }
}
