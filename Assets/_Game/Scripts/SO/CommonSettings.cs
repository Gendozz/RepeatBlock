using UnityEngine;

[CreateAssetMenu(menuName = "RepeatBlock/CommonSettings")]
public class CommonSettings : ScriptableObject
{
    [Header("Moves amount and duration")]
    public int opponentMovesAmountOnStart;

    public float movesIncreaseRate;

    public float maxMoves;

    public float moveDuration;

    [Header("Player&Opponent")]
    public float yHeight;

}
