//using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public class ApplicationFocusHandler : MonoBehaviour
{
        private AudioHandler _audioHandler;

    //    [DllImport("__Internal")]
    //    private static extern void PrintToConsole(string textToprint);

    [Inject]
    public void Construct(AudioHandler audioHandler)
    {
        _audioHandler = audioHandler;
//#if !UNITY_EDITOR
//            PrintToConsole("ApplicationFocusHandler Construct called"); 
//#endif
    }

    private void OnApplicationFocus(bool focus)
    {
        //Debug.Log("OnApplicationFocus = " + focus);
        //#if !UNITY_EDITOR
        //            PrintToConsole("OnApplicationFocus = " + focus); 
        //#endif

#if UNITY_EDITOR
        PauseAudio(!focus); 
#endif
    }

//    private void OnApplicationPause(bool pause)
//    {
//#if !UNITY_EDITOR
//            PrintToConsole("OnApplicationPause = " + pause);

//#endif
//        //Debug.Log("OnApplicationPause");
//        PauseAudio(pause);
//    }

    public void PauseAudio(bool toPause)
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
