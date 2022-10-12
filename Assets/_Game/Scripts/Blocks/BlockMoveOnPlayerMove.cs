using UnityEngine;
using Zenject;
using DG.Tweening;
using System;

public class BlockMoveOnPlayerMove : IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly BlockView _blockView;

    private Settings _settings;

    public BlockMoveOnPlayerMove(SignalBus signalBus, Settings settings, BlockView blockView)
    {
        _signalBus = signalBus;
        _settings = settings;
        _blockView = blockView;
    }
    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(MoveBlockView);
    }

    private void MoveBlockView(PlayerMoved args)
    {
        DirectionToMove directionToMove = args.direction;

        if (directionToMove == DirectionToMove.Right)
        {
            _blockView.transform.DOMove(_blockView.transform.position - Vector3.right, _settings.moveDuration).SetEase(Ease.Linear);
            return;
        }

        if (directionToMove == DirectionToMove.Left)
        {
            _blockView.transform.DOMove(_blockView.transform.position - Vector3.forward, _settings.moveDuration).SetEase(Ease.Linear);
        }
    }

    [Serializable]
    public class Settings
    {
        public float moveDuration;
    }
}
