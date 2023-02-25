using Zenject;

public class ScoreCounter : IInitializable
{
    private readonly SignalBus _signalBus;

    public int CurrentScore { get; private set; } = 0;

    public int MaxScore { get; private set; } = 0;

    public ScoreCounter(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(AddScore);
    }

    private void AddScore()
    {
        CurrentScore++;
        _signalBus.Fire(new ScoreChanged { score = CurrentScore });

        if(CurrentScore > MaxScore) 
        {
            MaxScore = CurrentScore;
            _signalBus.Fire(new MaxScoreChanged { maxScore = MaxScore });
        }
    }

    public void FillScoresOnStart(Save save)
    {
        MaxScore = save.MaxScores;
        CurrentScore = 0;
        _signalBus.Fire(new ScoreChanged { score = CurrentScore });
        _signalBus.Fire(new MaxScoreChanged { maxScore = MaxScore });
    }
}

