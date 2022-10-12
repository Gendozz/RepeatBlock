using System;
using Zenject;

public class AllBlocksMovedChecker : IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly BlocksSpawner _blocksSpawner;

    private int _blocksAmountToCheck;

    private int _blocksMovedAmount;

    public AllBlocksMovedChecker(SignalBus signalBus, BlocksSpawner blocksSpawner)
    {
        _signalBus = signalBus;
        _blocksSpawner = blocksSpawner;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<BlocksGenerationCompleted>(SetCurrentBlockAmount);
        _signalBus.Subscribe<BlockMoved>(CheckBlocks);
    }

    private void SetCurrentBlockAmount()
    {
        _blocksAmountToCheck = (int)_blocksSpawner.BlocksInSequence;
    }

    private void CheckBlocks()
    {
        _blocksMovedAmount++;

        if (_blocksMovedAmount == _blocksAmountToCheck)
        {
            _signalBus.Fire<AllBlocksMoved>();
            _blocksMovedAmount = 0;
        }
    }

    [Serializable]
    public class Settings
    {
        public float duration;
    }
}