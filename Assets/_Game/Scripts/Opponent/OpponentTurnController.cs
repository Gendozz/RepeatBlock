using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// 1. Определяет куда двигаться (направление и позиция)
/// 2. Говорит оппонентФасаду, что надо двигаться
/// 3. Говорит спавнеру, где надо спавнить
/// </summary>
public class OpponentTurnController : IAbleToPause
{
    private OpponentFacade _opponentFacade;

    private DirectionsProvider _directionsProvider;

    private PositionsProvider _positionsProvider;

    private BlocksSpawner _blocksSpawner;

    private GameStateMachine _gameStateMachine;

    private CommonSettings _commonSettings;

    private float _currentMovesAmountFloat;

    private bool _isGamePaused = false;

    private int CurrentMovesAmountInt
    {
        get
        {
            return (int)_currentMovesAmountFloat;
        }
    }

    public OpponentTurnController(
        OpponentFacade opponentFacade,
        DirectionsProvider directionsProvider,
        PositionsProvider positionsProvider,
        BlocksSpawner blocksSpawner,
        GameStateMachine gameStateMachine,
        CommonSettings commonSettings)
    {
        _opponentFacade = opponentFacade;
        _directionsProvider = directionsProvider;
        _positionsProvider = positionsProvider;
        _blocksSpawner = blocksSpawner;
        _gameStateMachine = gameStateMachine;
        _commonSettings = commonSettings;
        _currentMovesAmountFloat = _commonSettings.opponentMovesAmountOnStart;
    }

    public async UniTaskVoid DoOpponentTurnActions()
    {
        //Debug.Log("New opponent turn");

        SetMovesAmount();

        _positionsProvider.ClearPositions();

        Direction newDirection;

        // Renew position where opponent is staying due to it has moved by X and Z before opponent turn state started
        Vector3 newPosition = _positionsProvider.GetNewPositionFromPosition(_directionsProvider.GetDirection(true), Vector3.zero);

        bool shouldBlockMoveDownAfterSpawn = true;

        for (int i = 0; i < CurrentMovesAmountInt; i++)
        {
            CancellationToken ct = new CancellationToken();
            
            while (_isGamePaused)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_commonSettings.moveDuration), cancellationToken: ct);
            }            

            newDirection = _directionsProvider.GetNewDirection();
            newPosition = _positionsProvider.GetNewPosition(newDirection);

            if(i == CurrentMovesAmountInt - 1)
            {
                shouldBlockMoveDownAfterSpawn = false;
            }

            _blocksSpawner.SpawnBlock(newPosition, shouldBlockMoveDownAfterSpawn);

            await UniTask.WhenAll(TellOpponentFacadeToMove(newDirection, newPosition, ct));
        }

        _gameStateMachine.ChangeState<PlayerTurnState>();
    }

    private List<UniTask> TellOpponentFacadeToMove(Direction newDirection, Vector3 newPosition, CancellationToken ct)
    {
        return _opponentFacade.MoveOpponent(newDirection, newPosition, ct);
    }

    private void SetMovesAmount()
    {
        if (CurrentMovesAmountInt < _commonSettings.maxMoves)
        {
            _currentMovesAmountFloat += _commonSettings.movesIncreaseRate;
        }
    }

    public void Pause()
    {
        _isGamePaused = true;
    }

    public void Unpause()
    {
        _isGamePaused = false;
    }
}