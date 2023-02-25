using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsUIHandler : IInitializable
{
    private Button _switchSoundButton;
    private Button _switchMusicButton;
    private Button _closeSettingButton;

    private CanvasGroup _settingsCanvasGroup;

    private PauseService _pauseService;

    private AudioHandler _audioHandler;

    private SettingsPics _settingsPics;

    public SettingsUIHandler(
        [Inject(Id = UI_SettingsElementsIDs.SwitchSoundButton)] Button switchSoundButton,
        [Inject(Id = UI_SettingsElementsIDs.SwitchMusicButton)] Button switchMusicButton,
        [Inject(Id = UI_SettingsElementsIDs.CloseSettingsButton)] Button closeSettingButton,
        CanvasGroup settingsCanvasGroup,
        PauseService pauseService,
        AudioHandler audioHandler,
        SettingsPics settingsPics)
    {
        _switchSoundButton = switchSoundButton;
        _switchMusicButton = switchMusicButton;
        _closeSettingButton = closeSettingButton;
        _settingsCanvasGroup = settingsCanvasGroup;
        _pauseService = pauseService;
        _audioHandler = audioHandler;
        _settingsPics = settingsPics;
    }

    public void Initialize()
    {
        _switchSoundButton.onClick.AddListener(SwitchSound);
        _switchMusicButton.onClick.AddListener(SwitchMusic);
        _closeSettingButton.onClick.AddListener(CloseSettings);
    }

    public void RefreshSettingsPics()
    {
        ChangeMusicButtonPicture(_audioHandler.ShouldMusicPlay);
        ChangeSoundsButtonPicture(_audioHandler.ShouldSoundsPlay);
    }

    private void SwitchMusic()
    {
        bool isOn = _audioHandler.SwitchMusic();        
        ChangeMusicButtonPicture(isOn);
    }

    private void ChangeMusicButtonPicture(bool isOn)
    {
        switch (isOn)
        {
            case true:
                _switchMusicButton.GetComponent<Image>().sprite = _settingsPics._musicOn;
                break;
            case false:
                _switchMusicButton.GetComponent<Image>().sprite = _settingsPics._musicOff;
                break;
        }
    }

    private void SwitchSound()
    {
        bool isOn = _audioHandler.SwitchSounds();
        ChangeSoundsButtonPicture(isOn);
    }

    private void ChangeSoundsButtonPicture(bool isOn)
    {
        switch (isOn)
        {
            case true:
                _switchSoundButton.GetComponent<Image>().sprite = _settingsPics._soundsOn;
                break;
            case false:
                _switchSoundButton.GetComponent<Image>().sprite = _settingsPics._soundsOff;
                break;
        }
    }


    private void CloseSettings()
    {
        _settingsCanvasGroup.interactable = false;
        _settingsCanvasGroup.blocksRaycasts = false;
        _settingsCanvasGroup.alpha = 0;
        _pauseService.UnpauseAll();
    }
}
