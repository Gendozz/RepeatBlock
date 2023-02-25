[System.Serializable]
public class Save
{
    public int MaxScores { get; private set; }

    public Save()
    {
        MaxScores = 0;
    }

    public Save(int maxScores)
    {
        MaxScores = maxScores;
    }
}