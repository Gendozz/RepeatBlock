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
        _signalBus.Subscribe<PlayerFinishedPath>(SpawnNewBlockPath);
        //SpawnInitialBlocks();
        //SpawnBlock(_settings.InitialBlockPosition);                   // Was in start of development/ Now it's starts after tutorial
        //SpawnNewBlockPath();
        //SpawnTutorialPath();
    }

    public Transform SpawnInitialPlayerBlock(Vector3 playerBlockPosition)
    {
        return SpawnInitialBlock(playerBlockPosition);
        
    }
    
    public Transform SpawnInitialOpponentBlock(Vector3 opponentPosition)
    {        
        return SpawnInitialBlock(opponentPosition);
    }

    public void SpawnInitialPath()
    {
        Vector3[] positionsToSpawn = _pathGenerator.GenerateInitialPath();

        for (int i = 0; i < positionsToSpawn.Length - 1; i++)
        {
            SpawnBlock(positionsToSpawn[i]);
        }
    }

    private async void SpawnNewBlockPath()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_settings.DelayBeforeStartNextSpawnWave));

        Vector3[] positionsToSpawn = _pathGenerator.GenerateNewPath();
                
        foreach (var blockPosition in positionsToSpawn)
        {
            SpawnBlock(blockPosition);
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.DelayBetweenNextSpawnInCurrentWave));
        }
    }

    private void SpawnBlock(Vector3 position)
    {
        var blockFacade = _blockFactory.Create();
        blockFacade.Position = position;
    }

    private Transform SpawnInitialBlock(Vector3 position)
    {
        var blockFacade = _blockFactory.Create();
        blockFacade.Position = position;
        return blockFacade.transform;
    }

    [Serializable]
    public class Settings
    {
        public float DelayBetweenNextSpawnInCurrentWave;
        public float DelayBeforeStartNextSpawnWave;
        public Vector3 InitialBlockPosition;
    }
}