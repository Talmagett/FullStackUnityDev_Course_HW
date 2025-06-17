using System;
using System.Collections.Generic;
using UnityEditor;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public partial class SceneEntity : MonoBehaviour, IEntity
    {
        public event Action OnStateChanged
        {
            add => this.Entity.OnStateChanged += value;
            remove => this.Entity.OnStateChanged -= value;
        }

        public string Name
        {
            get => this.Entity.Name;
            set => this.Entity.Name = value;
        }

        public int Id
        {
            get { return this.Entity.Id; }
            set { this.Entity.Id = value; }
        }

        public bool Installed
        {
            get { return _entity == null; }
        }

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [HideInPlayMode]
#endif
        [Tooltip("If this option is enabled, the Install() method will be called on Awake()")]
        [SerializeField]
        private bool installOnAwake = true;

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0)]
        [GUIColor(1f, 0.92156863f, 0.015686275f)]
        [HideInPlayMode]
        [InfoBox(
            "WARINING: If you create Unity objects or another heavy objects in the Install() method, be sure to turn off!",
            InfoMessageType.Warning,
            nameof(installInEditMode))
        ]
#endif
        [Tooltip(
            "If this option is enabled, the Install() method will be called every time OnValidate is called in Edit Mode")]
        [SerializeField]
        private bool installInEditMode;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Tooltip("Should dispose values when OnDestroy() called")]
        [SerializeField]
        private bool disposeValues = true;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Tooltip("Specify the installers that will put values and systems to this context")]
        [Space, SerializeField]
        private List<SceneEntityInstaller> installers;

        internal Entity Entity
        {
            get
            {
                if (_entity == null) this.Install();
                return _entity;
            }
        }

        private Entity _entity;

        public void Install()
        {
            if (_entity != null)
                return;

            _entity = new Entity(this.name, _tagCapacity, _valueCapacity, _behaviourCapacity, this);
            this.InstallInternal();
            s_sceneEntities.TryAdd(_entity, this);
        }

        private void InstallInternal()
        {
            if (this.installers == null)
                return;

            for (int i = 0, count = this.installers.Count; i < count; i++)
            {
                SceneEntityInstaller installer = this.installers[i];
                if (installer != null)
                    installer.Install(this);
                else
                    Debug.LogWarning("SceneEntity: Ops! Detected null installer!", this);
            }
        }

        protected virtual void Awake()
        {
            if (this.installOnAwake) this.Install();
        }

        protected virtual void OnDestroy()
        {
            if (_entity == null)
                return;

            if (this.disposeValues) _entity.DisposeValues();
            _entity.UnsubscribeAll();
            s_sceneEntities.Remove(_entity);
        }

        public override string ToString()
        {
            return this.Entity.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is IEntity entity && this.Entity.Id == entity.Id;
        }

        public override int GetHashCode()
        {
#if UNITY_EDITOR
            try
            {
                return _entity != null ? _entity.GetHashCode() : -1;
            }
            catch (UnityException e)
            {
                return -1;
            }
#else
            return this.Entity.GetHashCode();
#endif
        }

        public void Clear()
        {
            this.Entity.Clear();
        }
    }
}