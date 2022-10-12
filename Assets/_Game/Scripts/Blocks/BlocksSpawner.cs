using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;

public class BlocksSpawner : IInitializable
{
    private readonly BlockFacade.Factory _blockFactory;
    private readonly Settings _settings;
    private readonly SignalBus _signalBus;

    private Vector3 _firstBlockPosition;

    public float BlocksInSequence { get; private set; }

    public Vector3[] CurrentBlockSequence { get; private set; }

    public DirectionToMove[] directionsSequence { get; private set; }

    public BlocksSpawner(
        Settings settings,
        SignalBus signalBus,
        BlockFacade.Factory blockFactory)
    {
        _settings = settings;
        _signalBus = signalBus;
        _blockFactory = blockFactory;

        BlocksInSequence = _settings.NumBlocksStartAmount;
        _firstBlockPosition = new Vector3(0, _settings.yHeightCreatePosition, 0);
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerFinishedSequence>(SpawnBlockSequence);
        SpawnBlockSequence();
    }

    private async void SpawnBlockSequence()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_settings.DelayBeforeStartNextSpawnWave));

        CurrentBlockSequence = GenerateNewSequence();

        _signalBus.Fire<BlocksGenerationCompleted>();

        for (int i = 0; i < CurrentBlockSequence.Length; i++)
        {
            SpawnBlock(CurrentBlockSequence[i]);
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.DelayBetweenNextSpawnInCurrentWave));
        }
    }

    private Vector3[] GenerateNewSequence()
    {
        //Debug.Log($"Generation started. FirstBlockPosition = {_firstBlockPosition}");

        Vector3[] blocksPositions = new Vector3[(int)BlocksInSequence];

        directionsSequence = new DirectionToMove[(int)BlocksInSequence];

        blocksPositions[0] = _firstBlockPosition;                // ATTENTION

        int oneDirectionCounter = 0;

        DirectionToMove previuosDirectionToSpawn = (DirectionToMove)Random.Range(0, 2);

        for (int i = 1; i < blocksPositions.Length; i++)
        {
            DirectionToMove currentDirectionToSpawn = (DirectionToMove)Random.Range(0, 2);

            if (oneDirectionCounter == 0)
            {
                previuosDirectionToSpawn = currentDirectionToSpawn;
                oneDirectionCounter++;
            }
            else
            {
                if (previuosDirectionToSpawn.Equals(currentDirectionToSpawn))
                {
                    oneDirectionCounter++;
                }
                else
                {
                    oneDirectionCounter = 0;
                }
            }

            if (oneDirectionCounter > _settings.maxOneSideSequence && previuosDirectionToSpawn.Equals(currentDirectionToSpawn))
            {
                currentDirectionToSpawn = (DirectionToMove)Mathf.Abs((int)currentDirectionToSpawn);
                previuosDirectionToSpawn = currentDirectionToSpawn;
                oneDirectionCounter = 0;
            }

            switch (currentDirectionToSpawn)
            {
                case DirectionToMove.Left:
                    blocksPositions[i] = blocksPositions[i - 1] + Vector3.forward;
                    break;
                case DirectionToMove.Right:
                    blocksPositions[i] = blocksPositions[i - 1] + Vector3.right;
                    break;
                default:
                    break;
            }

            directionsSequence[i] = currentDirectionToSpawn;
        }


        //Debug.Log($"FirstBlockPosition = {_firstBlockPosition}. blocksPositions[directionsSequence.Length - 1] = {blocksPositions[directionsSequence.Length - 1]}");

        //_firstBlockPosition = blocksPositions[directionsSequence.Length - 1];        

        //Debug.Log($"Generation ended. FirstBlockPosition = {_firstBlockPosition}.");
        return blocksPositions;
    }

    private void SpawnBlock(Vector3 position)
    {
        var blockFacade = _blockFactory.Create();
        blockFacade.Position = position;
    }

    [Serializable]
    public class Settings
    {
        public float NumBlocksStartAmount;
        public float NumBlocksIncreaseRate;
        public float maxSequence;

        public int maxOneSideSequence;

        public float DelayBetweenNextSpawnInCurrentWave;
        public float DelayBeforeStartNextSpawnWave;

        public float yHeightCreatePosition;
    }
}
