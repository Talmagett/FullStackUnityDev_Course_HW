namespace Modules.BehaviourTree
{
    public abstract class BehaviourNode : IBehaviourNode
    {
        public bool IsRunning => _isRunning;

        private bool _isRunning;

        public BehaviourResult Run(float deltaTime)
        {
            if (!_isRunning)
            {
                _isRunning = true;
                this.OnEnable();
            }

            BehaviourResult result = this.OnUpdate(deltaTime);

            if (result != BehaviourResult.RUNNING)
            {
                _isRunning = false;
                this.OnDisable();
            }

            return result;
        }

        public void Abort()
        {
            if (!_isRunning) 
                return;
            
            _isRunning = false;
            this.OnAbort();
            this.OnDisable();
        }

        protected abstract BehaviourResult OnUpdate(float deltaTime);
        
        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnAbort()
        {
        }
    }
}