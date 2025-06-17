using System;

namespace Modules.FSM
{
    public static class StateMachineExtensions
    {
        public static bool TryChangeState<T>(this IStateMachine fsm, Action transition = null) where T : IState =>
            fsm.TryChangeState(typeof(T), transition);

        public static void ChangeState<T>(this IStateMachine fsm, Action transition = null) where T : IState =>
            fsm.ChangeState(typeof(T), transition);

        public static void AddState<T>(this IStateMachine fsm, T state) where T : IState =>
            fsm.AddState(typeof(T), state);

        public static bool RemoveState<T>(this IStateMachine fsm) where T : IState =>
            fsm.RemoveState(typeof(T));

        public static bool ContainsState<T>(this IStateMachine fsm) where T : IState =>
            fsm.ContainsState(typeof(T));
    }
}