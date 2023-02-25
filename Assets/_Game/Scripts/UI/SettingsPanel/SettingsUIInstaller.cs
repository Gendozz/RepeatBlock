using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsUIInstaller : MonoInstaller<SettingsUIInstaller>
{
    public Button _switchSounds;

    public Button _switchMusic;

    public Button _closeButton;

    public SettingsPics _settingsPics;

    public override void InstallBindings()
    {
        BindUIElements();

        Container.BindInterfacesAndSelfTo<SettingsUIHandler>().AsSingle().WithArguments(_settingsPics);
    }

    private void BindUIElements()
    {
        Container.Bind<Button>().WithId(UI_SettingsElementsIDs.SwitchSoundButton).FromInstance(_switchSounds);
        Container.Bind<Button>().WithId(UI_SettingsElementsIDs.SwitchMusicButton).FromInstance(_switchMusic);
        Container.Bind<Button>().WithId(UI_SettingsElementsIDs.CloseSettingsButton).FromInstance(_closeButton);
    }
}

public enum UI_SettingsElementsIDs
{
    SwitchSoundButton,
    SwitchMusicButton,
    CloseSettingsButton
}