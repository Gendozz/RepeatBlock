using System;
using UnityEngine;
using Zenject;

public class BlockFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    private BlockView _view;
    private BlockTunables _blockTunables;
    private BlockMove _blockMove;
    IMemoryPool _pool;

    [Inject]
    public void Construct(BlockView view, BlockTunables blockTunables, BlockMove blockMove)
    {
        _view = view;
        _blockTunables = blockTunables;
        _blockMove = blockMove;
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
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
        //Debug.Log($"BlockFacade {gameObject.name} spawned in {transform.position}");
        _blockMove.Move();
    }

    public class Factory : PlaceholderFactory<BlockFacade>
    {
    }
}
