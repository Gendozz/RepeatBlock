using System;
using DG.Tweening;

public class PlayerDeathHandler : IAbleToPause
{
    private readonly PlayerView _playerView;
    
    public Settings _settings;

    private Tween _moveTween;

    public PlayerDeathHandler(
        PlayerView playerView, 
        Settings settings)
    {
        _playerView = playerView;
        _settings = settings;
    }

    public void Die()
    {
        _moveTween = _playerView.transform.DOMoveY(_settings.fallToYPosition, _settings.loseFallingDuration);
    }

    public void Pause()
    {
        _moveTween.Pause();
    }

    public void Unpause()
    {
        _moveTween.Play();
    }

    [Serializable]
    public class Settings 
    {
        public float loseFallingDuration;
        public float fallToYPosition;
    }
}
