using Zenject;

public class FindMatchesCommand : IChainCommand
{
    private IChainCommand _nextCommand;

    public FindMatchesCommand(IChainCommand nextCommand, DiContainer diContainer)
    {
        _nextCommand = nextCommand;
    }

    public void Execute(BundleData bundle)
    {
        // Implementation for finding matches

    }

    public IChainCommand Next()
    {
        return _nextCommand;
    }
}