public struct PathGenerationCompleted
{
    public int WaypointsAmount;
}

public struct InitialActionsDone { }

public struct OppenentFinishedPath { }

public struct PlayerMoved
{
    public DirectionToMove Direction;
}

public struct PlayerFinishedPath { }

public struct PlayerDied { }