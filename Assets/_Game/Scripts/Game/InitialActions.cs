using UnityEngine;
using DG.Tweening;
using System;
using Zenject;

public class InitialActions : IInitializable
{
    private readonly Settings _settings;

    private readonly PathGenerator _pathGenerator;

    private readonly BlocksSpawner _blocksSpawner;

    private readonly PlayerView _playerView;

    private readonly OpponentView _opponentView;

    private readonly SignalBus _signalBus;

    private Transform _playerStartBlockTransfrom;

    private Transform _opponentStartBlockTransfrom;

    private Vector3 _opponentStartPosition;

    public InitialActions(
        Settings settings,
        PathGenerator pathGenerator,
        BlocksSpawner blocksSpawner,
        PlayerView playerView,
        OpponentView opponentView,
        SignalBus signalBus)
    {
        _settings = settings;
        _pathGenerator = pathGenerator;
        _blocksSpawner = blocksSpawner;
        _playerView = playerView;
        _opponentView = opponentView;
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _playerStartBlockTransfrom = _blocksSpawner.SpawnInitialPlayerBlock(
            new Vector3(_playerView.transform.position.x, -15, _playerView.transform.position.z));

        _opponentStartBlockTransfrom = _blocksSpawner.SpawnInitialOpponentBlock(
            new Vector3(_opponentView.transform.position.x, -15, _opponentView.transform.position.z)); // TODO: Make not hardcoded
    }

    public void DoInitialActions()
    {
        Debug.Log("Initial actions started");
        _blocksSpawner.SpawnInitialPath();
        SetOpponentStartPosition();
        MoveChractersToStartPositions();
    }

    private void SetOpponentStartPosition()
    {
        Vector3 lastPointPosition = _pathGenerator.WaypointPositions[_pathGenerator.WaypointPositions.Length - 1];
        _opponentStartPosition = new Vector3(lastPointPosition.x, _settings.PlayerStartPosition.y, lastPointPosition.z);
    }

    public void MoveChractersToStartPositions()
    {
        float playerStartPositionX = _settings.PlayerStartPosition.x;
        float playerStartPositionZ = _settings.PlayerStartPosition.z;

        float opponentStartPositionX = _opponentStartPosition.x;
        float opponentStartPositionZ = _opponentStartPosition.z;


        _playerView.GetTransform.DOMove(_settings.PlayerStartPosition, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);
        _opponentView.GetTransform.DOMove(_opponentStartPosition, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);

        _playerStartBlockTransfrom.DOMoveX(playerStartPositionX, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);
        _playerStartBlockTransfrom.DOMoveZ(playerStartPositionZ, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);

        _opponentStartBlockTransfrom.DOMoveX(opponentStartPositionX, _settings.MoveToPositionsDuration).SetEase(Ease.Linear);
        _opponentStartBlockTransfrom.DOMoveZ(opponentStartPositionZ, _settings.MoveToPositionsDuration).SetEase(Ease.Linear).OnComplete(SayInitialActionsDone);
    }

    private void SayInitialActionsDone()
    {
        _signalBus.Fire(new InitialActionsDone());
    }

    [Serializable]
    public class Settings
    {
        public Vector3 PlayerStartPosition;
        public Vector3 PlayerBlockStartPosition;
        public float MoveToPositionsDuration;
    }
}
