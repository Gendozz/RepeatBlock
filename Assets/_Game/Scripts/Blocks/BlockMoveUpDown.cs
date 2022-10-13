using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class BlockMoveUpDown
{
    private readonly BlockView _view;

    private readonly SignalBus _signalBus;

    public Settings _settings = null;

    public BlockMoveUpDown(Settings settings, BlockView view, SignalBus signalBus)
    {
        _settings = settings;
        _view = view;
        _signalBus = signalBus;
    }

    public void Move()
    {
        _view.transform
            .DOMoveY(_settings.yHeight, _settings.moveDuration).OnComplete(FireMoveCompleted);
    }

    private void FireMoveCompleted()
    {
        _signalBus.Fire<BlockMoved>();
    }


    [Serializable]
    public class Settings
    {
        public float yHeight;
        public float moveDuration;
    }
}
