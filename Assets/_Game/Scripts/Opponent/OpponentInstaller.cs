using Zenject;

public class OpponentInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<OppenentMoveHandler>().AsSingle();
        Container.BindInterfacesTo<MoveTransformOnPlayerMove>().AsSingle();
        Container.Bind<RotateInDirection>().AsSingle();
    }
}