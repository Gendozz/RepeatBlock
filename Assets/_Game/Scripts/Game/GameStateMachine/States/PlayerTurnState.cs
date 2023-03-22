public class PlayerTurnState : IState
{
    private GameStateMachine _gameStateMachine;

    private PlayerFacade _playerFacade;

    private DirectionsProvider _directionsProvider;

    private ScoreCounter _scoreCounter;

    private CameraMovement _cameraMovement;

    private int _inputLimit;

    public PlayerTurnState(
        GameStateMachine gameStateMachine, 
        PlayerFacade playerFacade, 
        DirectionsProvider directionsProvider, 
        ScoreCounter scoreCounter,
        CameraMovement cameraMovement)
    {
        _gameStateMachine = gameStateMachine;
        _gameStateMachine.AddState(this);

        _playerFacade = playerFacade;
        _directionsProvider = directionsProvider;
        _scoreCounter = scoreCounter;
        _cameraMovement = cameraMovement;
    }

    public void Enter()
    {
        _inputLimit = _directionsProvider.DirectionsCount() - 1;

        _playerFacade.SetUserInputLimit(_inputLimit);
        _playerFacade.SetShouldDetectInput(true);
        _cameraMovement.SetPlayerAsTarget();

    }

    public void Exit()
    {
        _playerFacade.SetShouldDetectInput(false);
        Save save = new Save(_scoreCounter.MaxScore);
        SaveLoad.SaveProgress(save);
    }
}
