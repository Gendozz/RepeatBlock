using UnityEngine;
using Zenject;

public class PlayerFacade : MonoBehaviour, IAbleToPause
{
    private UserInputHandler _userInputHandler;

    private PlayerDeathHandler _playerDeathHandler;

    private PlayerMoveHandler _playerMoveHandler;

    private PauseService _pauseService;

    [Inject]
    public void Construct(
        UserInputHandler userInputHandler, 
        PlayerDeathHandler playerDeathHandler,
        PlayerMoveHandler playerMoveHandler,
        PauseService pauseService)
    {
        _userInputHandler = userInputHandler;
        _playerDeathHandler = playerDeathHandler;
        _playerMoveHandler = playerMoveHandler;
        _pauseService = pauseService;

        _pauseService.AddAbleToPause(this);
    }

    private void OnDestroy()
    {
        _pauseService.RemoveAbleToPause(this);
    }

    public void SetShouldDetectInput(bool shouldDetectInput)
    {
        _userInputHandler.SetShouldDetectInput(shouldDetectInput);
    }

    public void SetUserInputLimit(int inputLimit)
    {
        _userInputHandler.SetUserInputLimit(inputLimit);
    }

    public void PerformDie()
    {
        _playerDeathHandler.Die();
    }

    public void Pause()
    {
        _userInputHandler.Pause();
        _playerDeathHandler.Pause();
        _playerMoveHandler.Pause();
    }

    public void Unpause()
    {
        _userInputHandler.Unpause();
        _playerDeathHandler.Unpause();
        _playerMoveHandler.Unpause();
    }
}
