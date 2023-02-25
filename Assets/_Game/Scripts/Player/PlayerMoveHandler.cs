using Cysharp.Threading.Tasks;
using Zenject;

public class PlayerMoveHandler : ITickable, IAbleToPause
{
    private readonly CommonSettings _settings;

    private readonly PlayerMovesChecker _playerMovesChecker;

    private readonly RotateInDirection _rotateInDirection;

    private readonly SignalBus _signalBus;

    private readonly DirectionsQueue _userInputQueue;

    private GameStateMachine _gameStateMachine;

    public bool CanHandleMove
    {
        get
        {
            return _rotateInDirection.IsFreeToUse;
        }
    }

    public PlayerMoveHandler(
        CommonSettings settings,
        PlayerMovesChecker rightWayChecker, 
        RotateInDirection rotateInDirection, 
        SignalBus signalBus,
        DirectionsQueue directionsQueue,
        GameStateMachine gameStateMachine)
    {
        _settings = settings;
        _playerMovesChecker = rightWayChecker;
        _rotateInDirection = rotateInDirection;
        _signalBus = signalBus;
        _userInputQueue = directionsQueue;
        _gameStateMachine = gameStateMachine;
    }

    public void Tick()
    {
        if(!CanHandleMove) 
        {
            return;
        }

        if (_userInputQueue.HasNextInputDirections)
        {
            HandleMove(_userInputQueue.GetNextDirection());
        }
    }

    public void HandleMove(Direction directionToMove)
    {        
        _signalBus.Fire(new PlayerMoved { Direction = directionToMove });

        _rotateInDirection.HandleMoveDirection(directionToMove);

        bool isRightMove = _playerMovesChecker.CheckMove(directionToMove);

        if(!isRightMove)
        {
            WrongMoveActions();            
        }
    }

    private async UniTaskVoid WrongMoveActions()
    {
        await UniTask.WaitUntil(() => CanHandleMove == true);
        _gameStateMachine.ChangeState<LoseState>();
    }

    public void Pause()
    {
        _rotateInDirection.Pause();
    }

    public void Unpause()
    {
        _rotateInDirection.Unpause();
    }
}