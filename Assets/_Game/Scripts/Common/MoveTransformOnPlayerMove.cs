using Zenject;
using DG.Tweening;
using System;

public class MoveTransformOnPlayerMove : IInitializable, IAbleToPause
{
    private readonly SignalBus _signalBus;

    private readonly IView _view;

    private Settings _settings;

    //Tweener tween;

    private Tweener _moveXTween;
    private Tweener _moveZTween;

    public MoveTransformOnPlayerMove(SignalBus signalBus, Settings settings, IView view)
    {
        _signalBus = signalBus;
        _settings = settings;
        _view = view;
    }
    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(MoveView);
    }

    private void MoveView(PlayerMoved args)
    {
        Direction directionToMove = args.Direction;

        if (directionToMove == Direction.Right)
        {
            //tween = _view.GetTransform.DOMove(_view.GetTransform.position - Vector3.right, _settings.moveDuration).SetEase(Ease.Linear);
            _moveXTween = _view.GetTransform.DOMoveX(_view.GetTransform.position.x - 1, _settings.moveDuration).SetEase(Ease.Linear);

            return;
        }

        if (directionToMove == Direction.Left)
        {
            //tween = _view.GetTransform.DOMove(_view.GetTransform.position - Vector3.forward, _settings.moveDuration).SetEase(Ease.Linear);            
            _moveZTween = _view.GetTransform.DOMoveZ(_view.GetTransform.position.z - 1, _settings.moveDuration).SetEase(Ease.Linear);

        }
    }

    public void StopMoving()
    {
        //tween.Kill();
        _moveXTween.Kill();
        _moveZTween.Kill();
    }

    public void Pause()
    {
        //tween.Pause();
        _moveXTween.Pause();
        _moveZTween.Pause();
    }

    public void Unpause()
    {
        //tween.Play();
        _moveXTween.Play();
        _moveZTween.Play();
    }

    [Serializable]
    public class Settings
    {
        public float moveDuration;
    }
}