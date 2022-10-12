using UnityEngine;
using Zenject;

public class RightWayChecker : IInitializable
{
    readonly BlocksSpawner _blocksSpawner;

    readonly SignalBus _signalBus;

    private int _movesCounter = 0;

    public RightWayChecker(BlocksSpawner blocksSpawner, SignalBus signalBus)
    {
        _blocksSpawner = blocksSpawner;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(CheckMove);
    }

    private void CheckMove(PlayerMoved args)
    {
        _movesCounter++;
        DirectionToMove currentMove = args.direction;

        if (currentMove.Equals(_blocksSpawner.directionsSequence[_movesCounter]))
        {
            Debug.Log("Going right!");

            if (_movesCounter == _blocksSpawner.directionsSequence.Length - 1)
            {
                _signalBus.Fire<PlayerFinishedSequence>();
                Debug.Log("Finished reached");
                _movesCounter = 0;
            }
        }
        else
        {
            Debug.Log("Going wrong!!!");
            _signalBus.Fire<PlayerMovedWrongWay>();
        }
    }
}
