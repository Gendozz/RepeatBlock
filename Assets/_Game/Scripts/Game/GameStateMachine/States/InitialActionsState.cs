public class InitialActionsState : IState
{
    private GameStateMachine _gameStateMachine;

    private InitialActions _initialActions;

    public InitialActionsState(GameStateMachine gameStateMachine, InitialActions initialActions)
    {
        _gameStateMachine = gameStateMachine;
        _initialActions = initialActions;

        _gameStateMachine.AddState(this);
    }

    public void Enter()
    {
        _initialActions.DoInitialActions();
    }

    public void Exit()
    {

    }
}
