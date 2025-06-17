using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Modules.FSM
{
    public sealed class AutoStateMachineTests
    {
        [Test]
        public void Constructor_OppositeTransition_ShouldContainsBoth()
        {
            // Act:
            var stateMachine = new AutoStateMachine<string>("Patrol",
                new (string, IState)[]
                {
                    ("Patrol", new BaseState()),
                    ("Attack", new BaseState())
                },
                new StateTransition<string>[]
                {
                    new("Patrol", "Attack", 3),
                    new("Attack", "Patrol", 2)
                });

            // Assert
            Assert.AreEqual(2, stateMachine.TransitionCount);
            Assert.IsTrue(stateMachine.ContainsTransition("Patrol", "Attack"));
            Assert.IsTrue(stateMachine.ContainsTransition("Attack", "Patrol"));
        }

        [Test]
        public void AddTransition_ShouldIncreaseTransitionCount()
        {
            // Arrange
            var stateMachine = new AutoStateMachine<string>();

            // Act
            StateTransition<string> transition = new StateTransition<string>("Idle", "Move");
            bool added = stateMachine.AddTransition(transition);

            // Assert
            Assert.IsTrue(added);
            Assert.AreEqual(1, stateMachine.TransitionCount);
            Assert.IsTrue(stateMachine.ContainsTransition(transition));
        }

        [Test]
        public void RemoveTransition_ShouldDecreaseTransitionCount()
        {
            // Arrange
            var autoSM = new AutoStateMachine<string>(
                "Idle",
                new (string, IState)[]
                {
                    ("Idle", new BaseState()),
                    ("Move", new BaseState())
                },
                new StateTransition<string>[]
                {
                    new("Idle", "Move")
                });

            // Act
            bool removed = autoSM.RemoveTransition("Idle", "Move");

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(0, autoSM.TransitionCount);
        }

        [Test]
        public void OnUpdate_ShouldTriggerAutomaticTransition()
        {
            // Arrange
            bool wasTransition = false;

            var autoFSM = new AutoStateMachine<string>(
                "Idle",
                new KeyValuePair<string, IState>[]
                {
                    new("Idle", new BaseState()),
                    new("Move", new BaseState())
                }, new StateTransition<string>[]
                {
                    new("Idle", "Move", 1, () => true, () => wasTransition = true)
                }
            );

            string newState = null;
            autoFSM.OnStateChanged += state => newState = state;

            // Act
            autoFSM.OnUpdate(0.1f);

            // Assert
            Assert.IsTrue(wasTransition);
            Assert.AreEqual("Move", autoFSM.CurrentState);
            Assert.AreEqual("Move", newState);
        }

        [Test]
        public void OnUpdate_ShouldTriggerTransitionWithMaxPriority()
        {
            // Arrange
            var autoFSM = new AutoStateMachine<string>(
                "Idle",
                new KeyValuePair<string, IState>[]
                {
                    new("Idle", new BaseState()),
                    new("Patrol", new BaseState()),
                    new("Attack", new BaseState()),
                    new("Collect", new BaseState()),
                    new("Move", new BaseState())
                }, new StateTransition<string>[]
                {
                    new("Idle", "Move", 1, () => true),
                    new("Idle", "Patrol", 2, () => true),
                    new("Idle", "Attack", 5, () => true),
                    new("Idle", "Collect", 0, () => true)
                }
            );
            
            string newState = null;
            autoFSM.OnStateChanged += state => newState = state;
            
            // Act
            autoFSM.OnUpdate(0.1f);
            
            // Assert
            Assert.AreEqual("Attack", autoFSM.CurrentState);
            Assert.AreEqual("Attack", newState);
        }
        
        [Test]
        public void Add_Transitions_OnUpdate_ShouldTriggerTransitionWithMaxPriority()
        {
            // Arrange
            var stateMachine = new AutoStateMachine<string>(
                "Idle",
                new KeyValuePair<string, IState>[]
                {
                    new("Idle", new BaseState()),
                    new("Patrol", new BaseState()),
                    new("Attack", new BaseState()),
                    new("Collect", new BaseState()),
                    new("Move", new BaseState())
                }, Array.Empty<StateTransition<string>>()
            );

            stateMachine.AddTransition(new StateTransition<string>("Idle", "Move", 1, () => true));
            stateMachine.AddTransition(new StateTransition<string>("Idle", "Patrol", 2, () => true));
            stateMachine.AddTransition(new StateTransition<string>("Idle", "Attack", 5, () => true));
            stateMachine.AddTransition(new StateTransition<string>("Idle", "Collect", 0, () => true));

            string newState = null;
            stateMachine.OnStateChanged += state => newState = state;
            
            // Act
            stateMachine.OnUpdate(0.1f);
            
            // Assert
            Assert.AreEqual("Attack", stateMachine.CurrentState);
            Assert.AreEqual("Attack", newState);
        }
    }
}