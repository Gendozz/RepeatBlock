using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class OpponentFacade : MonoBehaviour, IAbleToPause
{
    private OpponentMovement _opponentMovement;

    private PauseService _pauseService;

    [Inject]
    public void Construct(OpponentMovement opponentMovement, PauseService pauseService)
    {
        _opponentMovement = opponentMovement;
        _pauseService = pauseService;

        _pauseService.AddAbleToPause(_opponentMovement);
    }

    private void OnDestroy()
    {
        _pauseService.RemoveAbleToPause(this);
    }

    public List<UniTask> MoveOpponent(Direction directionToRotate, Vector3 positionToVoveTo, CancellationToken ct)
    {
        return _opponentMovement.MoveOpponent(directionToRotate, positionToVoveTo, ct);
    }

    public void Pause()
    {
        _opponentMovement.Pause();
    }

    public void Unpause()
    {
        _opponentMovement.Unpause();
    }
}
