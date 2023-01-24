using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerFinishedPath>();
        Container.DeclareSignal<PlayerMoved>();
        Container.DeclareSignal<PlayerDied>();

        Container.DeclareSignal<PathGenerationCompleted>();

        Container.DeclareSignal<OppenentFinishedPath>();

        Container.DeclareSignal<InitialActionsDone>();
    }
}