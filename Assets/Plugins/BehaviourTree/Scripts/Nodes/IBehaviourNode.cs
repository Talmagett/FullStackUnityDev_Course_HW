namespace Modules.BehaviourTree
{
    public interface IBehaviourNode
    {
        bool IsRunning { get; }

        BehaviourResult Run(float deltaTime);
        
        void Abort();
    }
}