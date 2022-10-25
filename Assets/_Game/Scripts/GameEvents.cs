public struct NewPathRequired { }

public struct PathGenerationCompleted
{
    public int WaypointsAmount;
}

public struct BlockMoved { }

public struct AllBlocksMoved { }

public struct PlayerMoved
{
    public DirectionToMove Direction;
}

public struct PlayerFinishedPath { }

public struct PlayerMovedWrongWay { }