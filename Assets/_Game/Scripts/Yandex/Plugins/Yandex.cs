using System;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    public event Action AdOpenCallback;
    public event Action AdCloseCallback;

    public void OpenAd()
    {
        AdOpenCallback?.Invoke();
    }

    public void CloseAd()
    {
        AdCloseCallback?.Invoke();
    }
}
