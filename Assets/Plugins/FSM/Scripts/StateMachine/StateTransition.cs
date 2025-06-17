using System;
using System.Collections.Generic;

namespace Modules.FSM
{
    public readonly struct StateTransition<TKey> : IComparable<StateTransition<TKey>>
    {
        private static readonly EqualityComparer<TKey> s_keyComparer = EqualityComparer<TKey>.Default;

        public TKey From => this.from;
        public TKey To => this.to;
        
        internal readonly TKey from;
        internal readonly TKey to;

        private readonly int priority;
        private readonly Func<bool> condition;
        private readonly Action action;
        
        public StateTransition(TKey from, TKey to)
        {
            this.from = from;
            this.to = to;
            this.priority = 0;
            this.condition = null;
            this.action = null;
        }
        
        public StateTransition(TKey from, TKey to, Func<bool> condition)
        {
            this.from = from;
            this.to = to;
            this.priority = 0;
            this.condition = condition;
            this.action = null;
        }
        
        public StateTransition(TKey from, TKey to, int priority)
        {
            this.from = from;
            this.to = to;
            this.priority = priority;
            this.priority = 0;
            this.condition = null;
            this.action = null;
        }
        
        public StateTransition(TKey from, TKey to, int priority, Func<bool> condition)
        {
            this.from = from;
            this.to = to;
            this.priority = priority;
            this.condition = condition;
            this.action = null;
        }
        
        public StateTransition(TKey from, TKey to, Func<bool> condition, Action action)
        {
            this.from = from;
            this.to = to;
            this.priority = 0;
            this.condition = condition;
            this.action = action;
        }

        public StateTransition(TKey from, TKey to, int priority, Func<bool> condition, Action action)
        {
            this.from = from;
            this.to = to;
            this.priority = priority;
            this.condition = condition;
            this.action = action;
        }

        internal bool CanPerform() => this.condition == null || this.condition.Invoke();

        internal void Perform() => this.action?.Invoke();

        public int CompareTo(StateTransition<TKey> other) => other.priority.CompareTo(this.priority);
        
        public bool Equals(TKey from, TKey to)
        {
            return s_keyComparer.Equals(this.from, from) && s_keyComparer.Equals(this.to, to);
        }
        
        public bool Equals(StateTransition<TKey> other) => s_keyComparer.Equals(this.from, other.from) &&
                                                           s_keyComparer.Equals(this.to, other.to);
        
        public override bool Equals(object obj) => obj is StateTransition<TKey> other && Equals(other);
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (s_keyComparer.GetHashCode(this.from) * 397) ^
                       s_keyComparer.GetHashCode(this.to);
            }
        }
    }
}