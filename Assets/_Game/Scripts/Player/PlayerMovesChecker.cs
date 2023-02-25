public class PlayerMovesChecker
{
    private readonly DirectionsProvider _directionsProvider;

    private GameStateMachine _gameStateMachine;

    public PlayerMovesChecker(
        DirectionsProvider directionsProvider,
        GameStateMachine gameStateMachine)
    {
        _directionsProvider = directionsProvider;
        _gameStateMachine = gameStateMachine;
    }

    public bool CheckMove(Direction currentDirection)
    {
        if (currentDirection.Equals(_directionsProvider.GetDirection(false)))
        {
            if(_directionsProvider.DirectionsCount() <= 1)
            {
                ChangeState();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private  void ChangeState()
    {
        _gameStateMachine.ChangeState<OpponentTurnState>();
    }
}