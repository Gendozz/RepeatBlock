using System;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class PathRepeater : IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly PathGenerator _pathGenerator;

    private readonly OpponentView _opponentView;

    private readonly Settings _settings;

    private Vector3[] _repeatDirections;

    private int _repeatDirectionsLenght;

    public PathRepeater(SignalBus signalBus,
        PathGenerator pathGenerator,
        OpponentView opponentView,
        Settings settings)
    {
        _signalBus = signalBus;
        _pathGenerator = pathGenerator;
        _opponentView = opponentView;
        _settings = settings;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PathGenerationCompleted>(RepeatPath);
    }

    private void RepeatPath()
    {
        CopyTrimmedPath();
        ReversePath();      // REVERSE DUE TO LESS OPS IN FUTURE
        PerformRepeating();
    }

    private void CopyTrimmedPath()
    {
        _repeatDirectionsLenght = _pathGenerator.WaypointPositions.Length;
        _repeatDirections = new Vector3[_repeatDirectionsLenght];

        for (int i = 0; i < _repeatDirectionsLenght; i++)
        {
            Vector3 currentWaypoint = _pathGenerator.WaypointPositions[i];
            _repeatDirections[i] = new Vector3(currentWaypoint.x, _settings.yHeight, currentWaypoint.z);
        }
        //Array.Copy(_pathGenerator.WaypointPositions, 1, _repeatDirections, 0, _repeatDirectionsLenght);
    }

    private void ReversePath()
    {
        Array.Reverse(_repeatDirections);
    }

    private void PerformRepeating()
    {
        if (_repeatDirectionsLenght >= 0)
        {
            _opponentView.transform.DOMove(_repeatDirections[_repeatDirectionsLenght - 1], _settings.oneMoveDuration)
                .OnComplete(PerformRepeating);
            _repeatDirectionsLenght--;
        }
    }

    [Serializable]
    public class Settings
    {
        public float oneMoveDuration;
        public float yHeight;
    }
}