using UnityEngine;
using Zenject;

public class ApplicationFocusHandler : MonoBehaviour
{
    private AudioHandler _audioHandler;

    [Inject]
    public void Construct(AudioHandler audioHandler)
    {
        _audioHandler = audioHandler;
    }

    private void OnApplicationFocus(bool focus)
    {
        //Debug.Log("OnApplicationFocus = " + focus);
        PauseAudio(!focus);
    }

    private void OnApplicationPause(bool pause)
    {
        //Debug.Log("OnApplicationPause");
        PauseAudio(pause);
    }

    private void PauseAudio(bool toPause)
    {
        if (toPause)
        {
            _audioHandler.Pause();
        }
        else
        {
            _audioHandler.Unpause();
        }
    }
}
