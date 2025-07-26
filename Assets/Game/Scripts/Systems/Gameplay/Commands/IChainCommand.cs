public interface IChainCommand
{
    void Execute(BundleData bundle);
    IChainCommand Next();
}