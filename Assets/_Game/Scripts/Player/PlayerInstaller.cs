using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private CommonSettings _commonSettings;

    public override void InstallBindings()
    {
        Container.Bind<UserInputState>().AsSingle();
        Container.BindInterfacesAndSelfTo<UserInputHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<DirectionsQueue>().AsSingle();

        Container.Bind<RotateInDirection>().AsSingle().WithArguments(_commonSettings);
        Container.Bind<PlayerDeathHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().WithArguments(_commonSettings);
    }
}