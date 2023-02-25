using System.Collections.Generic;
using UnityEngine;

public class PositionsHolder                // TODO: Think to delete. It looks useless
{
    private Queue<Vector3> _positions = new Queue<Vector3>();

    public void EnqueuePosition(Vector3 position)
    {
        _positions.Enqueue(position);
    }

    public Vector3 DequeuePosition()
    {
        return _positions.Dequeue();
    }

    public Vector3 PeekPosition()
    {
        return _positions.Peek();
    }

    public int PositionsCount() { return _positions.Count; }

    public void ClearPositions()
    {
        _positions.Clear();
    }
}
