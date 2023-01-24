using UnityEngine;
using Zenject;
using DG.Tweening;
using System;

public class MoveTransformOnPlayerMove : IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly IView _view;

    private Settings _settings;

    public MoveTransformOnPlayerMove(SignalBus signalBus, Settings settings, IView view)
    {
        _signalBus = signalBus;
        _settings = settings;
        _view = view;
    }
    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(MoveBlockView);
    }

    private void MoveBlockView(PlayerMoved args)
    {
        DirectionToMove directionToMove = args.Direction;

        if (directionToMove == DirectionToMove.Right)
        {
            _view.GetTransform.DOMove(_view.GetTransform.position - Vector3.right, _settings.moveDuration).SetEase(Ease.Linear);
            return;
        }

        if (directionToMove == DirectionToMove.Left)
        {
            _view.GetTransform.DOMove(_view.GetTransform.position - Vector3.forward, _settings.moveDuration).SetEase(Ease.Linear);
        }
    }

    [Serializable]
    public class Settings
    {
        public float moveDuration;
    }
}
