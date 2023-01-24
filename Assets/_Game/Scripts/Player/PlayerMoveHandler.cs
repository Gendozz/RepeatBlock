using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class PlayerMoveHandler
{
    private readonly RightWayChecker _rightWayChecker;

    private readonly RotateInDirection _rotateInDirection;

    private readonly PlayerDeathHandler _playerDeathHandler;

    private readonly SignalBus _signalBus;

    public bool CanHandleMove
    {
        get
        {
            //Debug.Log("Acsessed to CanHandleMove prop. Returned " + _rotateInDirection.IsFreeToUse);
            return _rotateInDirection.IsFreeToUse;
        }
    }

    public PlayerMoveHandler(
        RightWayChecker rightWayChecker, 
        RotateInDirection rotateInDirection, 
        PlayerDeathHandler playerDeathHandler, 
        SignalBus signalBus)
    {
        _rightWayChecker = rightWayChecker;
        _rotateInDirection = rotateInDirection;
        _playerDeathHandler = playerDeathHandler;
        _signalBus = signalBus;
    }

    public void HandleMove(DirectionToMove directionToMove)
    {
        _rotateInDirection.HandleMoveDirection(directionToMove);
        _signalBus.Fire(new PlayerMoved { Direction = directionToMove });

        bool isRightMove = _rightWayChecker.CheckMove(directionToMove);

        if (!isRightMove)
        {
            WrongMoveActions();
        }
    }

    private async void WrongMoveActions()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_rotateInDirection._settings.moveDuration));
        _playerDeathHandler.Die();
    }
}
