using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameStarter _starter;
    //[SerializeField] private ApplicationFocusHandler _focusHandler;
    //[SerializeField] private Yandex _yandex;

    [Inject]
    private Settings _settings = null;

    [SerializeField] private CommonSettings _commonSettings;

    public override void Start()
    {
        base.Start();
        ResolveGameStates();
        Container.InstantiatePrefab(_starter);
        //Container.InstantiatePrefab(_focusHandler);
        //Container.InstantiatePrefab(_yandex);
    }

    private void ResolveGameStates()
    {
        Container.Resolve<GameLoadState>();
        Container.Resolve<MainMenuState>();
        Container.Resolve<InitialActionsState>();
        Container.Resolve<OpponentTurnState>();
        Container.Resolve<PlayerTurnState>();
        Container.Resolve<RestartGameState>();
        Container.Resolve<LoseState>();
    }

    public override void InstallBindings()
    {
        Container.Bind<PauseService>().AsSingle();

        //Container.Bind<ApplicationFocusHandler>().FromInstance(_focusHandler);

        Container.Bind<BlocksRegistry>().AsSingle();

        Container.BindInterfacesAndSelfTo<BlocksMoveHandler>().AsSingle();

        Container.Bind<LoadedDataBuffer>().AsSingle();

        Container.Bind<DirectionsProvider>().AsSingle();
        Container.Bind<PositionsProvider>().AsSingle();

        Container.Bind<PlayerMovesChecker>().AsSingle();

        Container.Bind<BlocksSpawner>().AsSingle();

        Container.Bind<RestartGame>().AsSingle();

        Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<OpponentView>().FromComponentInHierarchy().AsSingle();

        Container.Bind<OpponentTurnController>().AsSingle().WithArguments(_commonSettings);

        BindStateMachine();

        Container.BindInterfacesAndSelfTo<InitialActions>().AsSingle();

        Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle();

        Container.BindFactory<bool, BlockFacade, BlockFacade.Factory>()
            .FromPoolableMemoryPool<bool, BlockFacade, BlockFacadePool>(poolBinder => poolBinder
            .WithInitialSize(_settings.PoolInitialSize)
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<BlockInstaller>(_settings.BlockFacadePrefab)
            .UnderTransformGroup("Blocks"));

        GameSignalsInstaller.Install(Container);
    }

    private void BindStateMachine()
    {
        Container.Bind<GameStateMachine>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameLoadState>().AsSingle();
        Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
        Container.BindInterfacesAndSelfTo<InitialActionsState>().AsSingle();
        Container.BindInterfacesAndSelfTo<OpponentTurnState>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerTurnState>().AsSingle();
        Container.BindInterfacesAndSelfTo<RestartGameState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoseState>().AsSingle();
    }


    [Serializable]
    public class Settings
    {
        public GameObject BlockFacadePrefab;
        public int PoolInitialSize;
    }

    class BlockFacadePool : MonoPoolableMemoryPool<bool, IMemoryPool, BlockFacade>
    {

    }
}
