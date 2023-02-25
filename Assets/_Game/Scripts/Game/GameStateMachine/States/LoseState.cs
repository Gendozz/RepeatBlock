public class LoseState : IState
{
    private GameStateMachine _gameStateMachine;

    private PlayerFacade _playerFacade;

    private RestartGame _restartGame; // TODO: Put in GameRestartState    

    public LoseState(
        GameStateMachine gameStateMachine, 
        PlayerFacade playerFacade, 
        RestartGame restartGame)
    {
        _gameStateMachine = gameStateMachine;
        _playerFacade = playerFacade;
        _restartGame = restartGame;

        _gameStateMachine.AddState(this);
    }

    public void Enter()
    {
        _playerFacade.PerformDie();
        _restartGame.RestartScene();
    }

    public void Exit()
    {
    }
}

