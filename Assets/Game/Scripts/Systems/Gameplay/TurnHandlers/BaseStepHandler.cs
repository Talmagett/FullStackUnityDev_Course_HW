public abstract class BaseStepHandler : IStepHandler
{
    protected IStepHandler NextCommand => _nextCommand;
    private IStepHandler _nextCommand;
    public abstract void Execute(StepRequest bundle);

    public void SetNext(IStepHandler nextCommand)
    {
        _nextCommand = nextCommand;
    }
}