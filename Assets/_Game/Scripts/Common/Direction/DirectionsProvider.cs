public class DirectionsProvider
{
    private DirectionDeterminant _directionDeterminant;

    private DirectionsHolder _directionsHolder;

    public DirectionsProvider()
    {
        _directionDeterminant = new DirectionDeterminant();
        _directionsHolder = new DirectionsHolder();
    }

    public Direction GetNewDirection()
    {
        Direction newDirection = _directionDeterminant.GetNextDirection();
        _directionsHolder.EnqueueDirection(newDirection);
        return newDirection;
    }

    public Direction GetDirection(bool leaveSaved)
    {
        return leaveSaved ? _directionsHolder.PeekDirection() : _directionsHolder.DequeueDirection();
    }

    public int DirectionsCount()
    {
        return _directionsHolder.DirectionsCount();
    }
}
