using Zenject;

public class BlockInstaller : Installer<BlockInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<BlockTunables>().AsSingle();
        Container.Bind<BlockMoveUpDown>().AsSingle();
        Container.BindInterfacesTo<MoveTransformOnPlayerMove>().AsSingle();
        Container.BindInterfacesTo<BlockOutOfViewChecker>().AsSingle();
    }
}