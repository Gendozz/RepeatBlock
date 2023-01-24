using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerDeathHandler
{
    private readonly PlayerView _playerView;
    
    private readonly SignalBus _signalBus;

    public Settings _settings;

    public PlayerDeathHandler(PlayerView playerView, SignalBus signalBus, Settings settings)
    {
        _playerView = playerView;
        _signalBus = signalBus;
        _settings = settings;
    }

    public void Die()
    {
        Debug.Log("Player died");
        _playerView.transform.DOMoveY(_settings.fallToYPosition, _settings.loseFallingDuration);
        _signalBus.Fire<PlayerDied>();
    }

    [Serializable]
    public class Settings 
    {
        public float loseFallingDuration;
        public float fallToYPosition;
    }
}
