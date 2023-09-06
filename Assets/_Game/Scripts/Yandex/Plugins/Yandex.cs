using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void PrintToConsole(string textToprint);

    public event Action AdOpenCallback;
    public event Action AdCloseCallback;

    private AudioHandler _audioHandler;

    private bool _isAdvShowing = false;

    [Inject]
    public void Construct(AudioHandler audioHandler)
    {
        _audioHandler = audioHandler;
    }

    public void OpenAd()
    {
        PrintToConsole("Yandex - AdOpenCallback will be invoked");
        _isAdvShowing = true;
        AdOpenCallback?.Invoke();
    }

    public void CloseAd()
    {
        PrintToConsole("Yandex - AdCloseCallback will be invoked");
        _isAdvShowing = false;
        AdCloseCallback?.Invoke();
    }

    public void Pause()
    {
            PrintToConsole("Yandex - Pause() is calling");
            _audioHandler.Pause();   
    }

    public void Play()
    {
            PrintToConsole("Yandex - Play() is calling");
            _audioHandler.Unpause();         
    }

    public void Blur()
    {
        if (!_isAdvShowing)
        {
            PrintToConsole("Yandex - Blur() is calling");
            _audioHandler.Unpause(); 
        }
    }
    
    public void Focus()
    {
        if (!_isAdvShowing)
        {
            PrintToConsole("Yandex - Focus() is calling");
            _audioHandler.Unpause(); 
        }
    }
}
