using Zenject;

public class BlockInstaller : Installer<BlockInstaller>
{
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<BlockTunables>().AsSingle();
        Container.Bind<BlockMoveUpDown>().AsSingle();
        Container.BindInterfacesTo<BlockMoveOnPlayerMove>().AsSingle();
        Container.BindInterfacesTo<BlockOutOfViewChecker>().AsSingle();
    }
}