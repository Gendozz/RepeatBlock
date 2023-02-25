using UnityEngine;
using Zenject;

public class AudioInstaller : MonoInstaller<AudioInstaller>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;

    public override void InstallBindings()
    {
        Container.Bind<AudioHandler>().AsSingle();
        Container.Bind<AudioSource>().WithId(AudioSourcesID.MusicSource).FromInstance(_musicSource);
        Container.Bind<AudioSource>().WithId(AudioSourcesID.SoundsSource).FromInstance(_soundsSource);
    }
}

public enum AudioSourcesID
{
    MusicSource,
    SoundsSource
}