public struct PlayerMoved
{
    public Direction Direction;
}

public struct ScoreChanged
{
    public int score;
}

public struct MaxScoreChanged
{
    public int maxScore;
}

public struct DataLoaded
{
    public Save save;
}