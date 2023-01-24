using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainGuiInstaller : MonoInstaller<MainGuiInstaller>
{
    public Button _startButton;
    public Button _secondButton;

    public override void InstallBindings()
    {
        Container.Bind<Button>().WithId(ButtonsIDs.StartButton).FromInstance(_startButton);
        Container.Bind<Button>().WithId(ButtonsIDs.SecondButton).FromInstance(_secondButton);

        Container.BindInterfacesAndSelfTo<MainViewController>().AsSingle();
    }
}

public enum ButtonsIDs
{
    StartButton,
    SecondButton
}