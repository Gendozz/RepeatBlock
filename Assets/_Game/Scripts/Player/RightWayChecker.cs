using Zenject;

public class RightWayChecker
{
    private readonly PathGenerator _pathGenerator;

    private readonly SignalBus _signalBus;

    private int _movesCounter;

    public RightWayChecker(PathGenerator pathGenerator, SignalBus signalBus)
    {
        _pathGenerator = pathGenerator;
        _signalBus = signalBus;
    }

    public bool CheckMove(DirectionToMove currentDirection)
    {
        if (currentDirection.Equals(_pathGenerator.Directions[_movesCounter]))
        {
            //Debug.Log("Going right!");

            if (_movesCounter == _pathGenerator.Directions.Length - 1)
            {
                _signalBus.Fire<PlayerFinishedPath>();
                _movesCounter = 0;
                //Debug.Log($"Finished reached. _movesCounter set to {_movesCounter}");
                return true;
            }
            _movesCounter++;

            return true;

        }
        else
        {
            //Debug.Log($"Going wrong!!! CurrentMove is {currentMove}, need to be {_blocksPathGenerator.Directions[_movesCounter]}");
            return false;
        }

    }
}
