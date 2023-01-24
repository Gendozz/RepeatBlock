using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UserInputState>().AsSingle();

        Container.BindInterfacesAndSelfTo<UserInputHandler>().AsSingle();

        Container.BindInterfacesAndSelfTo<UserInputQueue>().AsSingle();

        Container.Bind<RotateInDirection>().AsSingle();

        Container.Bind<PlayerDeathHandler>().AsSingle();

        Container.Bind<PlayerMoveHandler>().AsSingle();
    }
}