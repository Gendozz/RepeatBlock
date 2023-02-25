using System.Collections.Generic;

public class PauseService
{
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
