using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    private Settings _settings = null;

    public override void InstallBindings()
    {        
        Container.BindInterfacesAndSelfTo<PathGenerator>().AsSingle();

        Container.Bind<RightWayChecker>().AsSingle();
        Container.BindInterfacesAndSelfTo<AllBlocksMovedChecker>().AsSingle();
        Container.BindInterfacesAndSelfTo<BlocksSpawner>().AsSingle();

        Container.BindInterfacesTo<RestartGame>().AsSingle();

        Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<OpponentView>().FromComponentInHierarchy().AsSingle();

        Container.BindInterfacesAndSelfTo<InitialActions>().AsSingle();

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
