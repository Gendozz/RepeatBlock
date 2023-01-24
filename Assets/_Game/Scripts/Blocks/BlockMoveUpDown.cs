using DG.Tweening;
using System;

public class BlockMoveUpDown
{
    private readonly BlockView _view;
    
    private readonly AllBlocksMovedChecker _allBlocksMovedChecker;

    public Settings _settings = null;

    public BlockMoveUpDown(
        Settings settings, 
        BlockView view,
        AllBlocksMovedChecker allBlocksMovedChecker)
    {
        _settings = settings;
        _view = view;
        _allBlocksMovedChecker = allBlocksMovedChecker;
    }

    public void Move()
    {
        _view.transform
            .DOMoveY(_settings.yHeight, _settings.moveDuration).OnComplete(FireMoveCompleted);
    }

    private void FireMoveCompleted()
    {
        _allBlocksMovedChecker.AddBlockAsChecked();
    }


    [Serializable]
    public class Settings
    {
        public float yHeight;
        public float moveDuration;
    }
}
