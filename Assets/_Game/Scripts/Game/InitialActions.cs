using UnityEngine;
using DG.Tweening;
using System;

public class InitialActions
{
    private readonly Settings _settings;

    private DirectionsProvider _directionsProvider;

    private PositionsProvider _positionsProvider;

    private readonly BlocksSpawner _blocksSpawner;

    private readonly PlayerView _playerView;

    private readonly OpponentView _opponentView;

    private GameStateMachine _gameStateMachine;

    private Transform _playerStartBlockTransfrom;

    private Transform _opponentStartBlockTransfrom;

    private Vector3 _opponentStartPosition;

    private BlockFacade _enemyInitiaBlock;

    private BlocksRegistry _blocksRegistry;

    private CameraMovement _cameraMovement;

    public InitialActions(
        Settings settings,
        DirectionsProvider directionsProvider,
        PositionsProvider positionsProvider,
        BlocksSpawner blocksSpawner,
        PlayerView playerView,
        OpponentView opponentView,
        GameStateMachine gameStateMachine,
        BlocksRegistry blocksRegistry,
        CameraMovement cameraMovement
        )
    {
        _settings = settings;
        _directionsProvider = directionsProvider;
        _positionsProvider = positionsProvider;
        _blocksSpawner = blocksSpawner;
        _playerView = playerView;
        _opponentView = opponentView;
        _gameStateMachine = gameStateMachine;
        _blocksRegistry = blocksRegistry;
        _cameraMovement = cameraMovement;
    }

    public void Initialize()
    {
        _playerStartBlockTransfrom = _blocksSpawner.SpawnBlock(_playerView.transform.position, false);
        _opponentStartBlockTransfrom = _blocksSpawner.SpawnBlock(_opponentView.transform.position, false);

        _enemyInitiaBlock = _blocksRegistry.PullOutLastBlock();
    }

    public void DoInitialActions()
    {
        Direction direction;
        Vector3 position = Vector3.zero;

        for (int i = 0; i < _settings.initialTrapAmount; i++)                 
        {
            direction = _directionsProvider.GetNewDirection();
            position = _positionsProvider.GetNewPosition(direction);
            if (i != _settings.initialTrapAmount - 1)
            {
                _blocksSpawner.SpawnBlock(position, false); 
            }
        }

        _blocksRegistry.AddBlock(_enemyInitiaBlock);

        _opponentStartPosition = position;
        MoveChractersToStartPositions();
    }

    public void MoveChractersToStartPositions()
    {
        float playerStartPositionX = _settings.PlayerStartPosition.x;
        float playerStartPositionZ = _settings.PlayerStartPosition.z;

        float opponentStartPositionX = _opponentStartPosition.x;
        float opponentStartPositionZ = _opponentStartPosition.z;

        // TODO: Implement arc movement http://forum.demigiant.com/index.php?topic=26.0
        _playerView.GetTransform.DOMove(_settings.PlayerStartPosition, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);
        _opponentView.GetTransform.DOMove(_opponentStartPosition, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);

        _playerStartBlockTransfrom.DOMoveX(playerStartPositionX, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);
        _playerStartBlockTransfrom.DOMoveZ(playerStartPositionZ, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);

        _opponentStartBlockTransfrom.DOMoveX(opponentStartPositionX, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);
        _opponentStartBlockTransfrom.DOMoveZ(opponentStartPositionZ, _settings.MoveToPositionsDuration).SetEase(Ease.Linear).OnComplete(GetToNextState);
    }

    private void GetToNextState()
    {
        _cameraMovement.StartMoving();
        _gameStateMachine.ChangeState<PlayerTurnState>();
    }

    [Serializable]
    public class Settings
    {
        public Vector3 PlayerStartPosition;
        public Vector3 PlayerBlockStartPosition;
        public float MoveToPositionsDuration;
        public int initialTrapAmount;
    }
}
