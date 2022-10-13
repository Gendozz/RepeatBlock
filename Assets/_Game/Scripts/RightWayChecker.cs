using UnityEngine;
using Zenject;

public class RightWayChecker : IInitializable
{
    private readonly BlockPathGenerator _blocksPathGenerator;

    private readonly SignalBus _signalBus;

    private int _movesCounter;

    public RightWayChecker(BlockPathGenerator blocksPathGenerator, SignalBus signalBus)
    {
        _blocksPathGenerator = blocksPathGenerator;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(CheckMove);
    }

    private void CheckMove(PlayerMoved args)
    {
        DirectionToMove currentMove = args.direction;

        if (currentMove.Equals(_blocksPathGenerator.Directions[_movesCounter]))
        {
            //Debug.Log("Going right!");

            if (_movesCounter == _blocksPathGenerator.Directions.Length - 1)
            {
                _signalBus.Fire<PlayerFinishedSequence>();
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
