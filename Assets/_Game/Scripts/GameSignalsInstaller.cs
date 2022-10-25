using UnityEngine;
using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerFinishedPath>();
        Container.DeclareSignal<PlayerMoved>();
        Container.DeclareSignal<PlayerMovedWrongWay>();

        Container.DeclareSignal<NewPathRequired>();
        Container.DeclareSignal<PathGenerationCompleted>();

        Container.DeclareSignal<BlockMoved>();
        Container.DeclareSignal<AllBlocksMoved>();


        //Container.BindSignal<BlocksGenerationCompleted>().ToMethod(() => Debug.Log("Test bind signals"));
    }
}