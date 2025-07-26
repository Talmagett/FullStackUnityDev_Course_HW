public class SwapTilesCommand : IChainCommand
{
    private IChainCommand _nextCommand;
    public SwapTilesCommand(IChainCommand nextCommand)
    {
        _nextCommand = nextCommand;
    }

    public void Execute(BundleData bundle)
    {
        // Implementation for swapping tiles
    }

    public IChainCommand Next()
    {
        return _nextCommand;
    }
}