using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainViewController : IInitializable
{
    private Button _startButton;

    private Button _secondButton;

    private InitialActions _actions;

    public MainViewController(
        [Inject(Id = ButtonsIDs.StartButton)] Button startButton,
        [Inject(Id = ButtonsIDs.SecondButton)] Button secondButton,
        InitialActions actions)
    {
        _startButton = startButton;
        _secondButton = secondButton;
        _actions = actions;
    }

    public void Initialize()
    {
        _startButton.onClick.AddListener(TestStartButton);
        _secondButton.onClick.AddListener(TestSecondButton);
    }

    private void TestStartButton()
    {
        Debug.Log("StartButtonPressed");
        _actions.DoInitialActions();
    } 
    
    private void TestSecondButton()
    {
        Debug.Log("SecondButtonPressed");
    }
}
