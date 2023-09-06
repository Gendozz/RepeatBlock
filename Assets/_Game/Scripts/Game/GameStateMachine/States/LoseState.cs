using Cysharp.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class LoseState : IState
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();

    private GameStateMachine _gameStateMachine;

    private PlayerFacade _playerFacade;

    private RestartGame _restartGame; // TODO: Put in GameRestartState    

    //private ApplicationFocusHandler _focusHandler;

    private Yandex _yandex;

    private bool _testWait;

    private bool _wasAdOpened;

    private CancellationTokenSource _cts;

    public LoseState(
        GameStateMachine gameStateMachine,
        PlayerFacade playerFacade,
        RestartGame restartGame,
        Yandex yandex
        //,ApplicationFocusHandler applicationFocusHandler
        )
    {
        _gameStateMachine = gameStateMachine;
        _playerFacade = playerFacade;
        _restartGame = restartGame;
        _yandex = yandex;
        //_focusHandler = applicationFocusHandler;

        _gameStateMachine.AddState(this);
        _yandex.AdOpenCallback += AdOpenedActions;
        _yandex.AdCloseCallback += AdClosedActions;
    }

    public void Enter()
    {
        _playerFacade.PerformDie();
        _cts = new CancellationTokenSource();

#if !UNITY_EDITOR
        ShowAdv(); 
#else
        Debug.Log("Ad Show Imitation");
        AdClosedActions();
#endif

    }

    public async void AdOpenedActions()
    {
        _wasAdOpened = true;
        _yandex.Pause();

        await UniTask.WaitUntil(() => _testWait == true, cancellationToken: _cts.Token);

        _restartGame.RestartScene();
    }

    public async void AdClosedActions()
    {
        _testWait = true;

        if (!_wasAdOpened)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            _restartGame.RestartScene(); 
        }
    }

    public void Exit()
    {
    }
}

