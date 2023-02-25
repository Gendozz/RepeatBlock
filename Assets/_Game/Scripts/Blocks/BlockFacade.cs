using System;
using UnityEngine;
using Zenject;

public class BlockFacade : MonoBehaviour, IPoolable<bool, IMemoryPool>, IDisposable, IAbleToPause
{
    private BlockView _view;
    private BlockTunables _blockTunables;
    private BlockMoveUpDown _blockMove;
    private MoveTransformOnPlayerMove _onPlayerMove;
    private IMemoryPool _pool;

    private PauseService _pauseService;

    private BlocksRegistry _blocksRegistry;

    private bool _shouldMoveDownAfterSpawn;

    [Inject]
    public void Construct(
        BlockView view,
        BlockTunables blockTunables,
        BlockMoveUpDown blockMove,
        MoveTransformOnPlayerMove onPlayerMove,
        PauseService pauseService,
        BlocksRegistry blocksRegistry)
    {
        _view = view;
        _blockTunables = blockTunables;
        _blockMove = blockMove;

        _onPlayerMove = onPlayerMove;

        _pauseService = pauseService;

        _blocksRegistry = blocksRegistry;
    }

    public Vector3 Position
    {
        get { return _view.Position; }
        set { _view.Position = value; }
    }

    public void Dispose()
    {
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        _pool = null;
        _pauseService.RemoveAbleToPause(this);
    }

    public void OnSpawned(bool shouldMoveDownAfterSpawn,IMemoryPool pool)
    {
        _pool = pool;
        _onPlayerMove.StopMoving();
        MoveOnSpawn(shouldMoveDownAfterSpawn);
        _pauseService.AddAbleToPause(this);
        _blocksRegistry.AddBlock(this);
    }

    public void MoveOnSpawn(bool shouldMoveDownAfterSpawn)
    {
        switch (shouldMoveDownAfterSpawn)
        {
            case true:
                MoveUpDown();
                break;
            case false:
                MoveUp();
                break;
        }
    }

    public void MoveUpDown()
    {
        _blockMove.MoveUpDown();
    }

    public void MoveDown()
    {
        _blockMove.MoveDown();
    }

    public void MoveUp()
    {
        _blockMove.MoveUp();
    }

    public void Pause()
    {
        _blockMove.Pause();
        _onPlayerMove.Pause();
    }

    public void Unpause()
    {
        _blockMove.Unpause();
        _onPlayerMove.Unpause();
    }

    public class Factory : PlaceholderFactory<bool, BlockFacade>
    {
    }
}
