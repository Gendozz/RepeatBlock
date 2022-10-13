using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BlockPathGenerator
{
    private Vector3[] _waypointPositions;

    private readonly Settings _settings;

    private readonly SignalBus _signalBus;

    private readonly Vector3 _firstBlockPosition;

    private DirectionToMove _directionFromTheFirstBlock;

    private int _waypointsIndex;

    public float BlocksAmountInPath { get; private set; }

    public DirectionToMove[] Directions { get; private set; }


    public BlockPathGenerator(Settings settings, SignalBus signalBus)
    {
        _settings = settings;
        _signalBus = signalBus;
        BlocksAmountInPath = _settings.numBlocksStartAmount;
        _firstBlockPosition = new Vector3(0, _settings.yHeightCreatePosition, 0);
    }

    public Vector3[] GenerateNewPath()
    {
        SetPathSize();
        SetFirstWayPointAndDirection();
        FillPathWithPositions();

        _signalBus.Fire(new BlocksGenerationCompleted { blocksGeneratedAmount = (int)BlocksAmountInPath });

        return _waypointPositions;
    }

    private void SetPathSize()
    {
        if (BlocksAmountInPath < _settings.maxSequence)
        {
            BlocksAmountInPath += _settings.numBlocksIncreaseRate;
        }

        _waypointPositions = new Vector3[(int)BlocksAmountInPath];
        Directions = new DirectionToMove[(int)BlocksAmountInPath];
    }

    private void SetFirstWayPointAndDirection()
    {
        _directionFromTheFirstBlock = (DirectionToMove)Random.Range(0, 2);

        _waypointPositions[0] = GetPosition(_firstBlockPosition, _directionFromTheFirstBlock);
        Directions[0] = _directionFromTheFirstBlock;
        _waypointsIndex = 1;
    }


    private void FillPathWithPositions()
    {
        DirectionToMove directionToMove = _directionFromTheFirstBlock;

        Vector3 previousPosition = _waypointPositions[0];

        while (_waypointsIndex < (int)BlocksAmountInPath)
        {
            int oneWayAmount = Random.Range(1, _settings.maxOneSideSequence);

            for (int i = 0; i < oneWayAmount; i++)
            {
                _waypointPositions[_waypointsIndex] = GetPosition(previousPosition, directionToMove);
                previousPosition = _waypointPositions[_waypointsIndex];
                Directions[_waypointsIndex] = directionToMove;
                _waypointsIndex++;

                if (_waypointsIndex >= (int)BlocksAmountInPath)
                {
                    break;
                }
            }

            directionToMove = (DirectionToMove)Mathf.Abs((int)directionToMove - 1); // change direction
        }
    }

    private Vector3 GetPosition(Vector3 previousPosition, DirectionToMove directionToMove)
    {
        switch (directionToMove)
        {
            case DirectionToMove.Left:
                return previousPosition + Vector3.forward;
            case DirectionToMove.Right:
                return previousPosition + Vector3.right;
            default:
                throw new ArgumentOutOfRangeException(nameof(directionToMove), directionToMove, null);
        }
    }

    [Serializable]
    public class Settings
    {
        public float numBlocksStartAmount;
        public float numBlocksIncreaseRate;
        public float maxSequence;
        public float yHeightCreatePosition;
        public int maxOneSideSequence;
    }
}