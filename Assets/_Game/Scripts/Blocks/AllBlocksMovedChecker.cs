using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class AllBlocksMovedChecker : IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly BlockPathGenerator _blocksPathGenerator;

    private int _blocksAmountToCheck;

    private int _blocksMovedAmount;

    public Settings _settings;

    public AllBlocksMovedChecker(SignalBus signalBus, BlockPathGenerator blocksPathGenerator, Settings settings)
    {
        _signalBus = signalBus;
        _blocksPathGenerator = blocksPathGenerator;
        _settings = settings;
    }

    public void Initialize()
    { _signalBus.Subscribe<BlocksGenerationCompleted>(SetCurrentBlockAmount);
        _signalBus.Subscribe<BlockMoved>(CheckBlocks);
    }

    private void SetCurrentBlockAmount()
    {
        _blocksAmountToCheck = (int)_blocksPathGenerator.BlocksAmountInPath;
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
        
        Debug.Log("BlockChecked");
    }

    [Serializable]
    public class Settings
    {
        public float allBlocksOffset;
    }
}