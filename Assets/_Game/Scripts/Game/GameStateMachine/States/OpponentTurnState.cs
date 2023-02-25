public class OpponentTurnState : IState
{
    private GameStateMachine _gameStateMachine;
    private OpponentTurnController _opponentTurnController;
    private PauseService _pauseService;
    private BlocksRegistry _blocksRegistry;
    private CameraMovement _cameraMovement;

    public OpponentTurnState(
        GameStateMachine gameStateMachine,
        OpponentTurnController opponentTurnController,
        PauseService pauseService,
        BlocksRegistry blocksRegistry,
        CameraMovement cameraMovement
        )
    {
        _gameStateMachine = gameStateMachine;
        _opponentTurnController = opponentTurnController;
        _pauseService = pauseService;
        _blocksRegistry = blocksRegistry;
        _cameraMovement = cameraMovement;

        _gameStateMachine.AddState(this);
    }

    public void Enter()
    {
        _blocksRegistry.GetBlockByIndex(_blocksRegistry.Count - 1).MoveDown();
        _opponentTurnController.DoOpponentTurnActions();
        _pauseService.AddAbleToPause(_opponentTurnController);
        _cameraMovement.SetOpponentAsTarget();
    }

    public void Exit()
    {
        _pauseService.RemoveAbleToPause(_opponentTurnController);
        _cameraMovement.SetPlayerAsTarget();
    }
}
