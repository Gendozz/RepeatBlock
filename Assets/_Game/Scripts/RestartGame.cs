using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using Zenject;

public class RestartGame : IInitializable, IDisposable
{
    readonly SignalBus _signalBus;

    public Settings _settings;

    public RestartGame(SignalBus signalBus, Settings settings)
    {
        _signalBus = signalBus;
        _settings = settings;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMovedWrongWay>(RestartScene);
    }
    
    public void Dispose()
    {
        _signalBus.Unsubscribe<PlayerMovedWrongWay>(RestartScene);
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
