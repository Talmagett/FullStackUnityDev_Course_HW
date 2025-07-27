public interface IStepHandler
{
    void SetNext(IStepHandler nextCommand);
    void Execute(StepRequest request);
}