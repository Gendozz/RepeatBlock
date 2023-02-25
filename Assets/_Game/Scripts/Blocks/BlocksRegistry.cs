using System.Collections.Generic;

public class BlocksRegistry
{
    readonly List<BlockFacade> _blocks = new List<BlockFacade>();

    public int Count { get { return _blocks.Count; } }

    public IEnumerable<BlockFacade> Enemies
    {
        get { return _blocks; }
    }

    public void AddBlock(BlockFacade block)
    {
        _blocks.Add(block);
    }

    public void RemoveBlock(BlockFacade block)
    {
        _blocks.Remove(block);
    }
    
    public void RemoveBlockByIndex(int index)
    {
        _blocks.RemoveAt(index);
    }    
    
    public BlockFacade PullOutBlockByIndex(int index)
    {
        BlockFacade blockFacade = _blocks[index];
        _blocks.RemoveAt(index);
        return blockFacade;
    }

    public BlockFacade PullOutLastBlock()
    {
        BlockFacade blockFacade = _blocks[_blocks.Count - 1];
        _blocks.Remove(blockFacade);
        return blockFacade;
    }

    public BlockFacade GetBlockByIndex(int index)
    {
        return _blocks[index];
    }
}