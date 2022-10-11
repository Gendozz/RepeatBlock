using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    Settings _settings = null;

    public override void InstallBindings()
    {
        //Container.Bind<UserInputState>().AsSingle();

        //Container.BindInterfacesAndSelfTo<PlayerMoveImitator>().AsSingle();

        //Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();

        Container.BindInterfacesAndSelfTo<BlocksSpawner>().AsSingle();

        Container.BindFactory<BlockFacade, BlockFacade.Factory>()
            .FromPoolableMemoryPool<BlockFacade, BlockFacadePool>(poolBinder => poolBinder
            .WithInitialSize(10)
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<BlockInstaller>(_settings.BlockFacadePrefab)
            .UnderTransformGroup("Blocks"));

        GameSignalsInstaller.Install(Container);
    }


    [Serializable]
    public class Settings
    {
        public GameObject BlockFacadePrefab;
    }

    class BlockFacadePool : MonoPoolableMemoryPool<IMemoryPool, BlockFacade>
    {

    }
}
