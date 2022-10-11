using UnityEngine;
using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<NewGenerationRequired>();
        Container.DeclareSignal<PlayerMoved>();

        Container.BindSignal<NewGenerationRequired>().ToMethod(() => Debug.Log("Test bind signals"));
    }
}