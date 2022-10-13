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
    private readonly BlockPathGenerator _blockPathGenerator;
    
    public BlocksSpawner(
        Settings settings,
        SignalBus signalBus,
        BlockFacade.Factory blockFactory,
        BlockPathGenerator blockPathGenerator)
    {
        _settings = settings;
        _signalBus = signalBus;
        _blockFactory = blockFactory;
        _blockPathGenerator = blockPathGenerator;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerFinishedSequence>(SpawnBlockSequence);
        SpawnBlock(new Vector3(0, -15, 0));                     // bring to settings
        SpawnBlockSequence();
    }

    private async void SpawnBlockSequence()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_settings.DelayBeforeStartNextSpawnWave));

        Vector3[] positionsToSpawn = _blockPathGenerator.GenerateNewPath();
        
        //Debug.Log($"BlockSpawner will spawn {positionsToSpawn.Length} blocks");
        
        foreach (var blockPosition in positionsToSpawn)
        {
            SpawnBlock(blockPosition);
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.DelayBetweenNextSpawnInCurrentWave));
        }
    }

    private void SpawnBlock(Vector3 position)
    {
        var blockFacade = _blockFactory.Create();
        //Debug.Log($"BlockSpawner: spawned block {blockFacade.gameObject.GetHashCode()} in position {blockFacade.Position} and will chang it's position to {position}");

        blockFacade.Position = position;

    }

    [Serializable]
    public class Settings
    {
        public float DelayBetweenNextSpawnInCurrentWave;
        public float DelayBeforeStartNextSpawnWave;
    }
}