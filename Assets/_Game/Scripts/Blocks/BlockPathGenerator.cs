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
        
        _signalBus.Fire<BlocksGenerationCompleted>();
        
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
        DirectionToMove directionFromTheFirstBlock = (DirectionToMove)Random.Range(0, 2);

        _waypointPositions[0] = GetPosition(_firstBlockPosition, directionFromTheFirstBlock);
        Directions[0] = directionFromTheFirstBlock;
    }


    private void FillPathWithPositions()
    {
        int waypointsIndex = 1;                     // CHANGE THIS TO PREVENT MORE THAN RESSTICTED AMOUNT IN ONE DIRECTION

        DirectionToMove directionToMove = (DirectionToMove)Random.Range(0, 2);

        Vector3 previousPosition = _waypointPositions[0];

        while (waypointsIndex < (int)BlocksAmountInPath)
        {
            int oneWayAmount = Random.Range(0, _settings.maxOneSideSequence);

            for (int i = 0; i < oneWayAmount; i++)
            {
                _waypointPositions[waypointsIndex] = GetPosition(previousPosition, directionToMove);
                previousPosition = _waypointPositions[waypointsIndex];
                Directions[waypointsIndex] = directionToMove;
                waypointsIndex++;
                
                if (waypointsIndex >= (int)BlocksAmountInPath)
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