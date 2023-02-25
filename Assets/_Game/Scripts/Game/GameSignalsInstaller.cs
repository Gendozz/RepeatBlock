using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PlayerMoved>();
        Container.DeclareSignal<ScoreChanged>();
        Container.DeclareSignal<MaxScoreChanged>();

        Container.DeclareSignal<DataLoaded>();
    }
}