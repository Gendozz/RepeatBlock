using Zenject;

public class BlocksMoveHandler : IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly BlocksRegistry _blocksRegistry;

    public BlocksMoveHandler(SignalBus signalBus, BlocksRegistry blocksRegistry)
    {
        _signalBus = signalBus;
        _blocksRegistry = blocksRegistry;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<PlayerMoved>(HandleBlockMove);
    }

    public void HandleBlockMove()
    {
        BlockFacade blockUnderPlayer = _blocksRegistry.PullOutBlockByIndex(0);
        blockUnderPlayer.MoveDown();
        blockUnderPlayer = _blocksRegistry.GetBlockByIndex(0);
        blockUnderPlayer.MoveUp();
    }
}
