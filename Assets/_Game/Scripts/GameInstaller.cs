using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    private Settings _settings = null;

    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {        
        Container.BindInterfacesAndSelfTo<BlockPathGenerator>().AsSingle();

        Container.BindInterfacesTo<RightWayChecker>().AsSingle();
        Container.BindInterfacesTo<AllBlocksMovedChecker>().AsSingle();
        Container.BindInterfacesAndSelfTo<BlocksSpawner>().AsSingle();

        Container.BindInterfacesTo<RestartGame>().AsSingle();

        Container.BindFactory<BlockFacade, BlockFacade.Factory>()
            .FromPoolableMemoryPool<BlockFacade, BlockFacadePool>(poolBinder => poolBinder
            .WithInitialSize(_settings.PoolInitialSize)
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<BlockInstaller>(_settings.BlockFacadePrefab)
            .UnderTransformGroup("Blocks"));

        GameSignalsInstaller.Install(Container);
    }


    [Serializable]
    public class Settings
    {
        public GameObject BlockFacadePrefab;
        public int PoolInitialSize;
    }

    class BlockFacadePool : MonoPoolableMemoryPool<IMemoryPool, BlockFacade>
    {

    }
}
