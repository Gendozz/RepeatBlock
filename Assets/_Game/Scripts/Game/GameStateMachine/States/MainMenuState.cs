public class MainMenuState : IState
{
    private readonly GameStateMachine _gameStateMachine;

    private readonly MainMenuViewController _mainViewController;

    private readonly SettingsUIHandler _settingsUIHandler;

    private readonly InitialActions _initialActions;

    private readonly LoadedDataBuffer _loadedDataBuffer;

    private readonly ScoreCounter _scoreCounter;

    public MainMenuState(
        GameStateMachine gameStateMachine, 
        MainMenuViewController mainViewController, 
        SettingsUIHandler settingsUIHandler,
        InitialActions initialActions,
        LoadedDataBuffer loadedDataBuffer, 
        ScoreCounter scoreCounter)
    {
        _gameStateMachine = gameStateMachine;
        _mainViewController = mainViewController;
        _settingsUIHandler = settingsUIHandler;
        _initialActions = initialActions;
        _loadedDataBuffer = loadedDataBuffer;
        _scoreCounter = scoreCounter;

        _gameStateMachine.AddState(this);
    }

    public void Enter()
    {                
        Save save = _loadedDataBuffer.Save;
        _scoreCounter.FillScoresOnStart(save);
        _settingsUIHandler.RefreshSettingsPics();

        _initialActions.Initialize();
    }

    public void Exit()
    {
        _mainViewController.ChangeOnInGameUI();
    }
}
