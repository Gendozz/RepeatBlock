using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class AllBlocksMovedChecker : IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly PathGenerator _pathGenerator;

    private int _blocksAmountToCheck;

    private int _blocksMovedAmount;

    public Settings _settings;

    public AllBlocksMovedChecker(SignalBus signalBus, PathGenerator pathGenerator, Settings settings)
    {
        _signalBus = signalBus;
        _pathGenerator = pathGenerator;
        _settings = settings;
    }

    public void Initialize()
    {
        _signalBus.Subscribe((PathGenerationCompleted args) => _blocksAmountToCheck = args.WaypointsAmount);
        _signalBus.Subscribe<BlockMoved>(CheckBlocks);
    }


    private async void CheckBlocks()
    {
        _blocksMovedAmount++;

        if (_blocksMovedAmount == _blocksAmountToCheck - _settings.allBlocksOffset)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.2));
            _signalBus.Fire<AllBlocksMoved>();
            _blocksMovedAmount = 0;
        }
    }

    [Serializable]
    public class Settings
    {
        public int allBlocksOffset;
    }
}