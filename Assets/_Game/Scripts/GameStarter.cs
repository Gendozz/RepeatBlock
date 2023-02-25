using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        _gameStateMachine.ChangeState<GameLoadState>();
    }
}
