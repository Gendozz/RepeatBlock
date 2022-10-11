using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;

public class BlocksSpawner : IInitializable
{
    readonly BlockFacade.Factory _blockFactory;
    readonly Settings _settings;
    readonly SignalBus _signalBus;

    private float _desiredNumBlocks;

    public BlocksSpawner(
        Settings settings, 
        SignalBus signalBus, 
        BlockFacade.Factory blockFactory)
    {
        _settings = settings;
        _signalBus = signalBus;
        _blockFactory = blockFactory;

        _desiredNumBlocks = _settings.NumBlocksStartAmount;        
    }

    public void Initialize()
    {
        _signalBus.Subscribe<NewGenerationRequired>(SpawnBlockSequence);
        SpawnBlockSequence();
    }

    private async void SpawnBlockSequence()
    {
        Vector3[] currentBlockSeqence = GenerateNewSequence();

        for (int i = 0; i < currentBlockSeqence.Length; i++)
        {
            SpawnBlock(currentBlockSeqence[i]);
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.DelayBetweenNextSpawnInCurrentWave));
        }
    }

    private Vector3[] GenerateNewSequence()
    {
        Vector3[] sequence = new Vector3[(int)_desiredNumBlocks];

        sequence[0] = new Vector3(0, _settings.yHeightCreatePosition, 0);

        //TODO: Add one direction limitation

        int sequnceCounter = 1;

        bool isPreviousToRight = false;

        for (int i = 1; i < sequence.Length; i++)
        {
            bool toRight = Random.Range(0f, 1f) > .5f;

            if(sequnceCounter == 1)
            {
                isPreviousToRight = toRight;
                sequnceCounter++;
            }
            else
            {
                if (isPreviousToRight.Equals(toRight))
                {
                    sequnceCounter++;
                }
            }

            if(sequnceCounter > _settings.maxOneSideSequence)
            {
                toRight = !toRight;
                isPreviousToRight = toRight;
                sequnceCounter = 1;
            }


            if (toRight)
            {
                sequence[i] = sequence[i-1] + Vector3.right;
            }
            else
            {
                sequence[i] = sequence[i - 1] + Vector3.forward;
            }
        }

        return sequence;
    }

    private void SpawnBlock(Vector3 position)
    {
        var blockFacade = _blockFactory.Create();
        blockFacade.Position = position;
        //Debug.Log($"BlocksSpawner was spawned blockFacade and changed it's position to {blockFacade.Position}");
    }

    [Serializable]
    public class Settings
    {
        public float NumBlocksStartAmount;
        public float NumBlocksIncreaseRate;
        public float maxSequence;

        public int maxOneSideSequence;

        public float DelayBetweenNextSpawnInCurrentWave;
        public float DelayBeforeStartNestSpawnWave;

        public float yHeightCreatePosition;
    }
}
