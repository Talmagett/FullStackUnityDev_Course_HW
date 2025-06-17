using System.Collections.Generic;
using Modules.FSM;
using NUnit.Framework;

namespace Modules.FSM
{
    public sealed class StateMachineTests
    {
        [Test]
        public void AddState_ShouldIncreaseStateCount()
        {
            // Arrange
            var stateMachine = new StateMachine<string>();
            var stateMock = new BaseState();

            // Act
            stateMachine.AddState("Idle", stateMock);

            // Assert
            Assert.AreEqual(1, stateMachine.StateCount);
            Assert.IsTrue(stateMachine.ContainsState("Idle"));
        }

        [Test]
        public void ChangeState_ShouldSwitchToNewState()
        {
            // Arrange
            var stateMachine = new StateMachine<string>();
            var idleState = new BaseState();
            var moveState = new BaseState();

            stateMachine.AddState("Idle", idleState);
            stateMachine.AddState("Move", moveState);

            // Act
            stateMachine.ChangeState("Move");

            // Assert
            Assert.AreEqual("Move", stateMachine.CurrentState);
        }

        [Test]
        public void ChangeState_ShouldCallOnExitAndOnEnter()
        {
            // Arrange
            var onExited = false;
            var onEntered = false;

            var stateMachine = new StateMachine<string>("Idle",
                new KeyValuePair<string, IState>("Idle", new BaseState(onExit: () => onExited = true)),
                new KeyValuePair<string, IState>("Move", new BaseState(onEnter: () => onEntered = true))
            );

            // Act
            stateMachine.ChangeState("Move");

            // Assert
            Assert.IsTrue(onExited);
            Assert.IsTrue(onEntered);
        }

        [Test]
        public void ChangeState_ShouldTriggerOnStateChangedEvent()
        {
            // Arrange
            var stateMachine = new StateMachine<string>();
            stateMachine.AddState("Idle", new BaseState());
            stateMachine.AddState("Move", new BaseState());

            string changedState = default;
            stateMachine.OnStateChanged += key => changedState = key;

            // Act
            stateMachine.ChangeState("Move");

            // Assert
            Assert.AreEqual("Move", changedState);
        }

        [Test]
        public void TryChangeState_ShouldReturnFalseIfSameState()
        {
            // Arrange
            var stateMachine = new StateMachine<string>("Idle",
                new KeyValuePair<string, IState>("Idle", new BaseState()),
                new KeyValuePair<string, IState>("Move", new BaseState())
            );

            // Act
            bool result = stateMachine.TryChangeState("Idle");

            // Assert
            Assert.False(result);
        }

        [Test]
        public void TryChangeState_ShouldReturnTrueIfNewState()
        {
            // Arrange
            var stateMachine = new StateMachine<string>("Idle",
                new KeyValuePair<string, IState>("Idle", new BaseState()),
                new KeyValuePair<string, IState>("Move", new BaseState())
            );

            // Act
            bool result = stateMachine.TryChangeState("Move");

            // Assert
            Assert.True(result);
        }

        
        [Test]
        public void RemoveState_ShouldDecreaseStateCount()
        {
            // Arrange
            var stateMachine = new StateMachine<string>("Idle",
                new KeyValuePair<string, IState>("Idle", new BaseState()),
                new KeyValuePair<string, IState>("Move", new BaseState())
            );
        
            // Act
            bool removed = stateMachine.RemoveState("Idle");
        
            // Assert
            Assert.True(removed);
            Assert.AreEqual(1, stateMachine.StateCount);
        }
        
        
        [Test]
        public void OnUpdate_ShouldCallCurrentStateUpdate()
        {
            var idleUpdate = false;
            var moveUpdate = false;
            
            // Arrange
            var stateMachine = new StateMachine<string>("Idle",
                new KeyValuePair<string, IState>("Idle", new BaseState(onUpdate: _ => idleUpdate = true)),
                new KeyValuePair<string, IState>("Move", new BaseState(onUpdate: _ => moveUpdate = true))
            );
            
            // Act
            stateMachine.OnUpdate(0.1f);
        
            // Assert
            Assert.IsTrue(idleUpdate);
            Assert.IsFalse(moveUpdate);
        }
    }
}