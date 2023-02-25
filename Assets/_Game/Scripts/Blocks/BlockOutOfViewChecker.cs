using System;
using Zenject;

public class BlockOutOfViewChecker : IInitializable
{
    private readonly BlockFacade _facade;

    private readonly SignalBus _signalBus;

    public Settings _settings;

    public BlockOutOfViewChecker(
        BlockFacade facade, 
        SignalBus signalBus, 
        Settings settings)
    {
        _facade = facade;
        _signalBus = signalBus;
        _settings = settings;
    }
    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(CheckIfOutOfView);
    }

    private void CheckIfOutOfView()
    {
        if(_facade.gameObject.activeSelf && 
           (_facade.transform.position.x < _settings.xOrZCoordToDispose || _facade.transform.position.z < _settings.xOrZCoordToDispose)
           )
        {
            _facade.Dispose();
        }
    }

    [Serializable]
    public class Settings
    {
        public float xOrZCoordToDispose;
    }
}
