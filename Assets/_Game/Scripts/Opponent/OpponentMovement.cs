using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class OpponentMovement : IAbleToPause
{
    private readonly OpponentView _opponentView;
    private readonly RotateInDirection _rotateInDirection;
    private readonly CommonSettings _settings;
    private readonly MoveTransformOnPlayerMove _moveTransformOnPlayerMove;

    private Tweener _moveXTween;
    private Tweener _moveZTween;

    public OpponentMovement(
        OpponentView opponentView, 
        RotateInDirection rotateInDirection,
        CommonSettings commonSettings,
        MoveTransformOnPlayerMove moveTransformOnPlayerMove)
    {
        _opponentView = opponentView;
        _rotateInDirection = rotateInDirection;
        _settings = commonSettings;
        _moveTransformOnPlayerMove = moveTransformOnPlayerMove;
    }

    public List<UniTask> MoveOpponent(Direction directionToRotate, Vector3 positionToMoveTo, CancellationToken ct)
    {
        RotateInDirectionOfMove(directionToRotate);

        List<UniTask> unitaskList = new List<UniTask>();

        _moveXTween = _opponentView.transform.DOMoveX(positionToMoveTo.x, _settings.moveDuration).SetEase(Ease.Linear);
        _moveZTween = _opponentView.transform.DOMoveZ(positionToMoveTo.z, _settings.moveDuration).SetEase(Ease.Linear);

        unitaskList.Add(_moveXTween.ToUniTask(cancellationToken: ct));
        unitaskList.Add(_moveZTween.ToUniTask(cancellationToken: ct));

        return unitaskList;
    }

    public void Pause()
    {
        _rotateInDirection.Pause();
        _moveXTween.Pause();
        _moveZTween.Pause();
        _moveTransformOnPlayerMove.Pause();
    }

    public void Unpause()
    {
        _rotateInDirection.Unpause();
        _moveXTween.Play();
        _moveZTween.Play();
        _moveTransformOnPlayerMove.Unpause();
    }

    private void RotateInDirectionOfMove(Direction directionToMove)
    {
        _rotateInDirection.HandleMoveDirection(directionToMove);
    }
}