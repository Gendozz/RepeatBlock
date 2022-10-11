using UnityEngine;
using Zenject;
using DG.Tweening;
using System;

public class MoveBlockGroupTransform : MonoBehaviour
{
    private SignalBus _signalBus;

    private Settings _settings;

    [Inject]
    public void Construct(SignalBus signalBus, Settings settings)
    {
        _signalBus = signalBus;
        _settings = settings;
    }

    private void Awake()
    {
        _signalBus.Subscribe<PlayerMoved>(MoveGroupTransform);
    }

    private void MoveGroupTransform(PlayerMoved args)
    {
        InputDirection asd = args.direction;

        if (asd == InputDirection.Right)
        {
            transform.DOMove(transform.position - Vector3.right, _settings.moveDuration).SetEase(Ease.Linear);
            return;
        }

        if (asd == InputDirection.Left)
        {
            transform.DOMove(transform.position - Vector3.forward, _settings.moveDuration).SetEase(Ease.Linear);
        }
    }

    [Serializable]
    public class Settings
    {
        public float moveDuration;
    }
}
