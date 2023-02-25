internal class GameLoadState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly LoadedDataBuffer _loadedDataBuffer;
    private readonly AudioHandler _audioHandler;

    public GameLoadState(GameStateMachine gameStateMachine, LoadedDataBuffer loadedDataBuffer, AudioHandler audioHandler)
    {
        _gameStateMachine = gameStateMachine;
        _loadedDataBuffer = loadedDataBuffer;
        _audioHandler = audioHandler;

        _gameStateMachine.AddState(this);
    }

    public void Enter()
    {   
        Save save = SaveLoad.LoadProgress();
        _loadedDataBuffer.WriteInBuffer(save); // ДЛЯ ЧЕГО ЭТО??????????
        _audioHandler.Initialize();
                
        _gameStateMachine.ChangeState<MainMenuState>();
    }

    public void Exit()
    {
        
    }
}
