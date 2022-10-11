using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UserInputState>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();

        Container.BindInterfacesTo<PlayerMoveImitator>().AsSingle();
    }
}