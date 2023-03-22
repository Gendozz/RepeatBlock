using Cysharp.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame
{
    [DllImport("__Internal")]
    private static extern void PrintToConsole(string textToprint);

    public Settings _settings;

    private bool _isAddShowing = false;

    public RestartGame(Settings settings)
    {
        _settings = settings;
    }

    public void SetIsAddShowingToTrue()
    {
        _isAddShowing = true;
    }

    public void RestartSceneAfterShowingAdd()
    {

        _isAddShowing = false;
        PrintToConsole("RestartSceneAfterShowingAdd - _isAddShowing = false");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartScene()
    {
        //await UniTask.Delay(TimeSpan.FromSeconds(_settings.delayBeforeRestart));

        //if (_isAddShowing)
        //{
        //    PrintToConsole("RestartScene - _isAddShowing = true");
        //    return;
        //}

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [Serializable]
    public class Settings
    {
        public float delayBeforeRestart;
    }
}