using System;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class OppenentMoveHandler : IInitializable
{        
    private readonly SignalBus _signalBus;

    private readonly PathGenerator _pathGenerator;

    private readonly OpponentView _opponentView;

    private readonly Settings _settings;

    private readonly RotateInDirection _rotateInDirection;

    private int _waypointsAmount;

    private int _currentMoveIndex;

    public OppenentMoveHandler(
        SignalBus signalBus,
        PathGenerator pathGenerator,
        OpponentView opponentView,
        Settings settings,
        RotateInDirection rotateInDirection)
    {
        _signalBus = signalBus;
        _pathGenerator = pathGenerator;
        _opponentView = opponentView;
        _settings = settings;
        _rotateInDirection = rotateInDirection;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PathGenerationCompleted>(WalkThePath);
    }

    private void WalkThePath()
    {
        Debug.Log("Oppenent started walking along the path.");
        GetWaypointsAmount();
        DecideToMove();
    }

    private void GetWaypointsAmount()
    {
        _waypointsAmount = (int)_pathGenerator.BlocksAmountInPath;
    }

    private  void DecideToMove()
    {
        if (_currentMoveIndex < _waypointsAmount)
        {
            MoveToNextWayPoint(_pathGenerator.WaypointPositions[_currentMoveIndex]);
            _currentMoveIndex++;
            return;
        }
        
        _signalBus.Fire<OppenentFinishedPath>();
        _currentMoveIndex = 0;
    }
    
    private void MoveToNextWayPoint(Vector3 waypointPosition)
    {
        Vector3 positionToMove = new Vector3(waypointPosition.x, _settings.yHeight, waypointPosition.z);
        RotateInDirectionOfMove(_pathGenerator.Directions[_currentMoveIndex]);
        
        _opponentView.transform.DOMoveX(positionToMove.x, _settings.oneMoveDuration).SetEase(Ease.Linear);
        _opponentView.transform.DOMoveZ(positionToMove.z, _settings.oneMoveDuration).SetEase(Ease.Linear).OnComplete(DecideToMove);        
    }

    private void RotateInDirectionOfMove(DirectionToMove directionToMove)
    {
        _rotateInDirection.HandleMoveDirection(directionToMove);
    }


    // private void CopyTrimmedPath()
    // {
    //     _repeatPositionsLenght = _pathGenerator.WaypointPositions.Length;
    //     _repeatPositions = new Vector3[_repeatPositionsLenght];
    //     _repeatDirections = new DirectionToMove[_repeatPositionsLenght];
    //
    //     for (int i = 0; i < _repeatPositionsLenght; i++)
    //     {
    //         Vector3 currentWaypoint = _pathGenerator.WaypointPositions[i];
    //         _repeatPositions[i] = new Vector3(currentWaypoint.x, _settings.yHeight, currentWaypoint.z);
    //         _repeatDirections[i] = _pathGenerator.Directions[i];
    //     }
    // }
    //
    // private void ReversePath()
    // {
    //     Array.Reverse(_repeatPositions);
    // }
    //
    // private void PerformRepeating()
    // {
    //     if (_repeatPositionsLenght >= 0)
    //     {
    //         _opponentView.transform.DOMove(_repeatPositions[_repeatPositionsLenght - 1], _settings.oneMoveDuration)
    //             .SetEase(Ease.Linear)
    //             .OnComplete(PerformRepeating);
    //         _moveImitator.HandleMoveDirection(_repeatDirections[_repeatPositionsLenght - 1]);
    //         _repeatPositionsLenght--;
    //     }
    // }

    [Serializable]
    public class Settings
    {
        public float oneMoveDuration;
        public float yHeight;
    }
}