using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using Zenject;

public class RestartGame : IInitializable
{
    private readonly SignalBus _signalBus;

    public Settings _settings;

    public RestartGame(SignalBus signalBus, Settings settings)
    {
        _signalBus = signalBus;
        _settings = settings;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerDied>(RestartScene);
    }

    private async void RestartScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_settings.delayBeforeRestart));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [Serializable]
    public class Settings
    {
        public float delayBeforeRestart;
    }

}
