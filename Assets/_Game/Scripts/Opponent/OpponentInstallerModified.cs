using Zenject;

public class OpponentInstallerModified : MonoInstaller
{
    public CommonSettings _commonSettings;

    public override void InstallBindings()
    {        
        Container.Bind<RotateInDirection>().AsSingle().WithArguments(_commonSettings);
        Container.Bind<OpponentMovement>().AsSingle().WithArguments(_commonSettings);
        Container.BindInterfacesAndSelfTo<MoveTransformOnPlayerMove>().AsSingle();
    }
}