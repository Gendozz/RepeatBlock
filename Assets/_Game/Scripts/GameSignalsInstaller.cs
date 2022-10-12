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

        Container.BindSignal<PlayerFinishedSequence>().ToMethod(() => Debug.Log("Test bind signals"));
    }
}