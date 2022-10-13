using System;
using UnityEngine;
using Zenject;

public class BlockFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    private BlockView _view;
    private BlockTunables _blockTunables;
    private BlockMoveUpDown _blockMove;
    private IMemoryPool _pool;

    [Inject]
    public void Construct(BlockView view, BlockTunables blockTunables, BlockMoveUpDown blockMove)
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
        //Debug.Log($"Facade transform from BlockFacade {transform.GetHashCode()}");

        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
        //Debug.Log($"BlockFacade {gameObject.GetHashCode()} spawned in {transform.position}");
        _blockMove.Move();
    }

    public class Factory : PlaceholderFactory<BlockFacade>
    {
    }
}
