using UnityEngine;
using Zenject;

public class RightWayChecker : IInitializable
{
    private readonly PathGenerator _pathGenerator;

    private readonly SignalBus _signalBus;

    private int _movesCounter;

    public RightWayChecker(PathGenerator pathGenerator, SignalBus signalBus)
    {
        _pathGenerator = pathGenerator;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(CheckMove);
    }

    private void CheckMove(PlayerMoved args)
    {
        DirectionToMove currentMove = args.Direction;

        if (currentMove.Equals(_pathGenerator.Directions[_movesCounter]))
        {
            //Debug.Log("Going right!");

            if (_movesCounter == _pathGenerator.Directions.Length - 1)
            {
                _signalBus.Fire<PlayerFinishedPath>();
                _movesCounter = 0;
                //Debug.Log($"Finished reached. _movesCounter set to {_movesCounter}");
                return;
            }            
            _movesCounter++;

        }
        else
        {
            //Debug.Log($"Going wrong!!! CurrentMove is {currentMove}, need to be {_blocksPathGenerator.Directions[_movesCounter]}");
            _signalBus.Fire<PlayerMovedWrongWay>();
        }

    }
}
