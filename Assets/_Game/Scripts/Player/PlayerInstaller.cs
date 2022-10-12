using Zenject;

public class PlayerInstaller : MonoInstaller
{
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<UserInputState>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();

        Container.BindInterfacesAndSelfTo<UserInputQueue>().AsSingle();

        Container.BindInterfacesTo<PlayerMoveImitator>().AsSingle();
    }
}