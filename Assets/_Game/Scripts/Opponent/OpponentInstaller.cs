using Zenject;

public class OpponentInstaller : MonoInstaller
{
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PathRepeater>().AsSingle();
    }
}