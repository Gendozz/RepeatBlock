using UnityEngine;

public class BlocksSpawner
{
    private readonly BlockFacade.Factory _blockFactory;
    
    public BlocksSpawner(BlockFacade.Factory blockFactory)
    {
        _blockFactory = blockFactory;
    }

    public Transform SpawnBlock(Vector3 position, bool shouldMoveDownAfterSpawn = true)
    {
        var blockFacade = _blockFactory.Create(shouldMoveDownAfterSpawn);
        //blockFacade._shouldMoveDownAfterSpawn = true;
        blockFacade.Position = ReplaceY(position);
        return blockFacade.transform;
    }

    private Vector3 ReplaceY(Vector3 vector3)
    {
        return new Vector3(vector3.x, -15, vector3.z); // TODO: Make not hardcoded
    }
}