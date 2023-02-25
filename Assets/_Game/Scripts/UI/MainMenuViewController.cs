using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuViewController : IInitializable, ITickable
{
    private Button _startButton;
    private Button _menuButton;

    private TMP_Text _currentScoreText;
    private TMP_Text _maxScoreText;
    private TMP_Text _gameNameText;

    private CanvasGroup _settingsCanvasGroup;

    private GameStateMachine _gameStateMachine;

    private SignalBus _signalBus;

    private PauseService _pauseService;

    public MainMenuViewController(
        [Inject(Id = UI_MainElementsIDs.StartButton)] Button startButton,
        [Inject(Id = UI_MainElementsIDs.MenuButton)] Button menuButton,
        [Inject(Id = UI_MainElementsIDs.CurrentScoreText)] TMP_Text currentScoresText,
        [Inject(Id = UI_MainElementsIDs.MaxScoreText)] TMP_Text maxScoresText,
        [Inject(Id = UI_MainElementsIDs.GameNameText)] TMP_Text gameNameText,        
        CanvasGroup canvasGroup,
        GameStateMachine gameStateMachine,
        SignalBus signalBus,
        PauseService pauseService)
    {
        _startButton = startButton;
        _menuButton = menuButton;
        _currentScoreText = currentScoresText;
        _maxScoreText = maxScoresText;
        _gameNameText = gameNameText;

        _settingsCanvasGroup = canvasGroup;
        _gameStateMachine = gameStateMachine;
        _signalBus = signalBus;
        _pauseService = pauseService;
    }

    public void Initialize()
    {
        HideSettingsMenu();

        _signalBus.Subscribe<ScoreChanged>(RenewCurrentScore);
        _signalBus.Subscribe<MaxScoreChanged>(RenewMaxScore);
        
        _startButton.onClick.AddListener(StartInitialActionsState);
        _menuButton.onClick.AddListener(ShowSettingsMenu);

        _startButton.GetComponentInChildren<Text>().DOFade(0.5f, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public void ChangeOnInGameUI()
    {
        _startButton.gameObject.SetActive(false);
        _gameNameText.gameObject.SetActive(false);
        _currentScoreText.gameObject.SetActive(true);
        _maxScoreText.gameObject.SetActive(true);
    }

    private void StartInitialActionsState()
    {
        _gameStateMachine.ChangeState<InitialActionsState>();
    } 

    private void ShowSettingsMenu()
    {
        _settingsCanvasGroup.alpha = 1;
        _settingsCanvasGroup.blocksRaycasts = true;
        _settingsCanvasGroup.interactable = true;
        _pauseService.PauseAll();
    }

    private void HideSettingsMenu()
    {
        _settingsCanvasGroup.alpha = 0;
        _settingsCanvasGroup.blocksRaycasts = false;
        _settingsCanvasGroup.interactable = false;
        _pauseService.UnpauseAll();
    }

    private void RenewCurrentScore(ScoreChanged args)
    {
        _currentScoreText.text = args.score.ToString();
    }
    
    private void RenewMaxScore(MaxScoreChanged args)
    {
        _maxScoreText.text = args.maxScore.ToString();
    }

    public void ChangeMaxScore(int maxScore)
    {
        _maxScoreText.text = maxScore.ToString();
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (_pauseService.IsGamePaused)
            {
                case true:
                    _pauseService.UnpauseAll();
                    HideSettingsMenu();
                    break;
                case false:
                    _pauseService.PauseAll();
                    ShowSettingsMenu();
                    break;
            }
        }
    }
}
