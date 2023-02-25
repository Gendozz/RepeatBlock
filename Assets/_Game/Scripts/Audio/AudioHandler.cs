using UnityEngine;
using Zenject;

public class AudioHandler : IAbleToPause
{
    private readonly AudioSource _musicSource;
    private readonly AudioSource _soundsSource;

    private readonly PauseService _pauseService;

    private bool _shouldMusicPlay;
    private bool _shouldSoundsPlay;

    private bool _isPaused;

    public bool ShouldMusicPlay
    {
        get { return _shouldMusicPlay; }
    }

    public bool ShouldSoundsPlay
    {
        get { return _shouldSoundsPlay; }
    }

    public AudioHandler(
        [Inject(Id = AudioSourcesID.MusicSource)] AudioSource musicSource,
        [Inject(Id = AudioSourcesID.SoundsSource)] AudioSource soundsSource,
        PauseService pauseService)
    {
        _musicSource = musicSource;
        _soundsSource = soundsSource;
        _pauseService = pauseService;
    }

    public void Initialize()
    {
        LoadAudioPlayerPrefs();
        ApplyPlayerPrefs();
    }

    private void LoadAudioPlayerPrefs()
    {
        _shouldMusicPlay = SaveLoadPlayerPrefs.LoadMusicPref();
        _shouldSoundsPlay = SaveLoadPlayerPrefs.LoadSoundsPref();
    }

    private void ApplyPlayerPrefs()
    {
        if (_shouldMusicPlay)
        {
            _musicSource.Play();
        }
    }

    public bool SwitchMusic()
    {
        _shouldMusicPlay = !_shouldMusicPlay;

        SaveLoadPlayerPrefs.SaveMusicPref(_shouldMusicPlay);

        if (_shouldMusicPlay)
        {
            _musicSource.Play();
        }
        else
        {
            _musicSource.Stop();
        }
        return _shouldMusicPlay;
    }

    public bool SwitchSounds()
    {
        _shouldSoundsPlay = !_shouldSoundsPlay;

        SaveLoadPlayerPrefs.SaveSoundsPref(_shouldSoundsPlay);
        return _shouldSoundsPlay;
    }

    public void PlaySound()
    {
        if (_shouldSoundsPlay)
        {
            _soundsSource.Play();
        }
    }

    public void Pause()
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Pause();
            _shouldMusicPlay = false;
            _shouldSoundsPlay = false;
            _isPaused = true;
        }
    }

    public void Unpause()
    {
        if (_isPaused)
        {
            _shouldMusicPlay = true;
            _shouldSoundsPlay = true;
            _musicSource.Play();
            _isPaused = false;
        }
    }
}
