using UnityEngine;
using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerFinishedSequence>();
        Container.DeclareSignal<PlayerMoved>();
        Container.DeclareSignal<PlayerMovedWrongWay>();

        Container.DeclareSignal<NewBlocksGenerationRequired>();
        Container.DeclareSignal<BlocksGenerationCompleted>();

        Container.DeclareSignal<BlockMoved>();
        Container.DeclareSignal<AllBlocksMoved>();


        //Container.BindSignal<BlocksGenerationCompleted>().ToMethod(() => Debug.Log("Test bind signals"));
    }
}