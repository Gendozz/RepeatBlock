using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class RestartGame
{
    public Settings _settings;

    public RestartGame(Settings settings)
    {
        _settings = settings;
    }

    public async UniTaskVoid RestartScene()
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