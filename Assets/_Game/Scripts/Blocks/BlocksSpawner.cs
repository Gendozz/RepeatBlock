using System;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

public class BlocksSpawner : IInitializable
{
    private readonly BlockFacade.Factory _blockFactory;
    private readonly Settings _settings;
    private readonly SignalBus _signalBus;
    private readonly PathGenerator _pathGenerator;
    
    public BlocksSpawner(
        Settings settings,
        SignalBus signalBus,
        BlockFacade.Factory blockFactory,
        PathGenerator pathGenerator)
    {
        _settings = settings;
        _signalBus = signalBus;
        _blockFactory = blockFactory;
        _pathGenerator = pathGenerator;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerFinishedPath>(SpawnBlockSequence);
        SpawnBlock(new Vector3(0, -15, 0));                     // bring to settings
        SpawnBlockSequence();
    }

    private async void SpawnBlockSequence()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_settings.DelayBeforeStartNextSpawnWave));

        Vector3[] positionsToSpawn = _pathGenerator.GenerateNewPath();
        
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