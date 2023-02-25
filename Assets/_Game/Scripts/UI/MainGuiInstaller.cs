using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainGuiInstaller : MonoInstaller<MainGuiInstaller>
{
    public Button _startButton;

    public Button _menuButton;

    public TMP_Text _currentScoresText;

    public TMP_Text _maxScoreText;

    public TMP_Text _gameNameText;

    public CanvasGroup _settingsCanvasGroup;

    public override void InstallBindings()
    {
        BindUIElements();

        Container.BindInterfacesAndSelfTo<MainMenuViewController>().AsSingle();
    }

    private void BindUIElements()
    {
        Container.Bind<Button>().WithId(UI_MainElementsIDs.StartButton).FromInstance(_startButton);
        Container.Bind<Button>().WithId(UI_MainElementsIDs.MenuButton).FromInstance(_menuButton);
        Container.Bind<TMP_Text>().WithId(UI_MainElementsIDs.CurrentScoreText).FromInstance(_currentScoresText);
        Container.Bind<TMP_Text>().WithId(UI_MainElementsIDs.MaxScoreText).FromInstance(_maxScoreText);
        Container.Bind<TMP_Text>().WithId(UI_MainElementsIDs.GameNameText).FromInstance(_gameNameText);
        Container.Bind<CanvasGroup>().FromInstance(_settingsCanvasGroup);
    }
}

public enum UI_MainElementsIDs
{
    StartButton,
    MenuButton,
    CurrentScoreText,
    MaxScoreText,
    GameNameText
}