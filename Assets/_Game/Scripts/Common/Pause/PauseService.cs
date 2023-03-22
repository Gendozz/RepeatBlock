using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PauseService
{
    [DllImport("__Internal")]
    private static extern void PrintToConsole(string textToprint);

    private List<IAbleToPause> _ableToPauseObjects;

    public bool IsGamePaused { get; private set; } = false;

    public PauseService()
    {
        _ableToPauseObjects= new List<IAbleToPause>();
    }

    public void AddAbleToPause(IAbleToPause ableToPause)
    {
        _ableToPauseObjects.Add(ableToPause);
    }

    public void RemoveAbleToPause(IAbleToPause ableToPause)
    {
        if(_ableToPauseObjects.Contains(ableToPause))
        {
            _ableToPauseObjects.Remove(ableToPause);
        }
    }

    public void PauseAll()
    {
        IsGamePaused = true;
        foreach (var ableToPauseObject in _ableToPauseObjects)
        {
            ableToPauseObject.Pause();
        }

#if !UNITY_EDITOR
        PrintToConsole("Pause Service - Pause All");

#endif
    }

    public void UnpauseAll()
    {
        IsGamePaused = false;
        foreach (var ableToPauseObject in _ableToPauseObjects)
        {
            ableToPauseObject.Unpause();
        }
    }
}
