using DG.Tweening;
using System;

public class BlockMoveUpDown : IAbleToPause
{
    private readonly BlockView _view;
    
    public Settings _settings = null;

    private Tween _tween;

    public BlockMoveUpDown(
        Settings settings, 
        BlockView view)
    {
        _settings = settings;
        _view = view;
    }

    public void MoveUpDown()
    {
        _tween = _view.transform.DOMoveY(_settings.yHeight, _settings.moveDuration * 1.5f).SetEase(Ease.OutExpo).OnComplete(MoveDown);
    }

    public void MoveUp()
    {
        _tween = _view.transform.DOMoveY(_settings.yHeight, _settings.moveDuration * 1.5f).SetEase(Ease.OutExpo);
    }

    public void MoveDown()
    {
        _tween = _view.transform.DOMoveY(-20, _settings.moveDuration * 1.5f).SetEase(Ease.InExpo); // TODO: Make not hardcoded
    }

    public void Pause()
    {
        _tween.Pause();
    }

    public void Unpause()
    {
        _tween.Play();
    }

    [Serializable]
    public class Settings
    {
        public float yHeight;
        public float moveDuration;
    }
}
