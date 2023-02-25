using UnityEngine;
using DG.Tweening;

public class RotateInDirection : IAbleToPause
{
    private IView _view;

    private readonly CommonSettings _commonSettings;

    private readonly AudioHandler _audioHandler;

    public bool IsFreeToUse { get; private set; } = true;

    private Direction _currentDirectionToMove;

    private Sequence _raiseWhenRotate;

    private Tweener _rotateTween;

    private Vector3 _rightRotation = new Vector3(0, 0, -90f);

    private Vector3 _leftRotation = new Vector3(90, 0, 0);


    public RotateInDirection(
        IView view,
        CommonSettings settings,
        AudioHandler audioHandler)
    {
        _view = view;
        _commonSettings = settings;
        _audioHandler = audioHandler;
    }

    public void HandleMoveDirection(Direction directionToMove)
    {
        _currentDirectionToMove = directionToMove;

        switch (_currentDirectionToMove)
        {
            case Direction.Left:
                RotateAndRaise(_leftRotation);
                break;
            case Direction.Right:
                RotateAndRaise(_rightRotation);
                break;
        }
    }

    private void RotateAndRaise(Vector3 neededRotation)
    {
        IsFreeToUse = false;  
        
        _audioHandler.PlaySound();

        _raiseWhenRotate = DOTween.Sequence();
        _raiseWhenRotate.Append(_view.GetTransform.DOMoveY(_commonSettings.yHeight, _commonSettings.moveDuration / 2))
            .SetLoops(2, LoopType.Yoyo);

        _rotateTween = _view.GetTransform
            .DORotate(_view.GetTransform.rotation.eulerAngles + neededRotation, _commonSettings.moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(EndMovingActions);
    }

    private void EndMovingActions()
    {

        _view.GetTransform.rotation = Quaternion.identity;

        IsFreeToUse = true;
    }

    public void Pause()
    {
        _raiseWhenRotate.Pause();
        _rotateTween.Pause();
    }

    public void Unpause()
    {
        _rotateTween.Play();
        _raiseWhenRotate.Play();

    }
}