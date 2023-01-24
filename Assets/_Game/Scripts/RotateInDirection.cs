using UnityEngine;
using DG.Tweening;
using System;

public class RotateInDirection
{
    private IView _view;

    public Settings _settings = null;

    public bool IsFreeToUse { get; private set; } = true;

    private DirectionToMove _currentDirectionToMove;

    private Sequence _moveImitation;

    private Vector3 _rightRotation = new Vector3(0, 0, -90f);

    private Vector3 _leftRotation = new Vector3(90, 0, 0);


    public RotateInDirection(
        IView view,
        Settings settings)
    {
        _view = view;
        _settings = settings;
    }

    public void HandleMoveDirection(DirectionToMove directionToMove)
    {
        _currentDirectionToMove = directionToMove;

        switch (_currentDirectionToMove)
        {
            case DirectionToMove.Left:
                RotateAndRaise(_leftRotation);
                break;
            case DirectionToMove.Right:
                RotateAndRaise(_rightRotation);
                break;
        }
    }

    private void RotateAndRaise(Vector3 neededRotation)
    {
        IsFreeToUse = false;
        _moveImitation = DOTween.Sequence();
        _moveImitation.Append(_view.GetTransform.DOMoveY(_settings.yMoveHeight, _settings.moveDuration / 2))
            .SetLoops(2, LoopType.Yoyo);

        _view.GetTransform
            .DORotate(_view.GetTransform.rotation.eulerAngles + neededRotation, _settings.moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(EndMovingActions);
    }

    private void EndMovingActions()
    {
        Debug.Log("View moved and free to use");
        _view.GetTransform.rotation = Quaternion.identity;

        IsFreeToUse = true;
    }

    [Serializable]
    public class Settings
    {
        public float yMoveHeight;
        public float moveDuration;
    }
}