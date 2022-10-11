using UnityEngine;
using DG.Tweening;
using Zenject;
using System;

public class PlayerMoveImitator : ITickable
{

    readonly PlayerView _playerView;

    readonly SignalBus _signalBus;

    readonly PlayerInputHandler _playerInputHandler;

    private bool _canMove = true;

    public Settings _settings = null;

    public PlayerMoveImitator(
        PlayerView playerView,
        Settings settings,
        SignalBus signalBus,
        PlayerInputHandler playerInputHandler)
    {
        _playerView = playerView;
        _settings = settings;
        _signalBus = signalBus;
        _playerInputHandler = playerInputHandler;
    }

    public void Tick()
    {
        if (!_canMove)
        {
            return;
        }
        if (!_playerInputHandler._HasNextinputDirections)
        {
            return;
        }


        HandleMoveDirection();
    }

    private void HandleMoveDirection()
    {
        InputDirection inputDirection = _playerInputHandler.GetNextDirection();

        _signalBus.Fire(new PlayerMoved { direction = inputDirection});


        switch (inputDirection)
        {
            case InputDirection.Left:
                ImitateMoving(new Vector3(90, 0, -0));
                break;
            case InputDirection.Right:
                ImitateMoving(new Vector3(0, 0, -90f));
                break;
            default:
                break;
        }
    }

    private void ImitateMoving(Vector3 neededRotation)
    {
        _canMove = false;
        Sequence moveImitation = DOTween.Sequence();
        moveImitation.Append(_playerView.transform.DOMoveY(_settings.yMoveHeight, _settings.moveDuration / 2)).SetLoops(2, LoopType.Yoyo);
        _playerView.transform.DORotate(_playerView.transform.rotation.eulerAngles + neededRotation, _settings.moveDuration).SetEase(Ease.Linear).OnComplete(ResetState);
    }

    private void ResetState()
    {
        _canMove = true;
        _playerView.transform.rotation = Quaternion.identity;
    }

    [Serializable]
    public class Settings
    {
        public float yMoveHeight;
        public float moveDuration;
    }

}
