using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UserInputQueue : IInitializable
{
    private readonly SignalBus _signalBus;

    private Queue<DirectionToMove> _inputDirections = new Queue<DirectionToMove>();

    private bool _shouldEnqueue;
    
    public UserInputQueue(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public bool HasNextInputDirections => _inputDirections.Count > 0;

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerFinishedSequence>(() => _shouldEnqueue = false);
        _signalBus.Subscribe<AllBlocksMoved>(() => _shouldEnqueue = true);
    }

    public void EnqueueInput(DirectionToMove directionToMove)
    {
        if (_shouldEnqueue)
        {
            _inputDirections.Enqueue(directionToMove);
            Debug.Log("Input enqueued");
        }
    }

    public DirectionToMove GetNextDirection()
    {
        return _inputDirections.Dequeue();
    }
}