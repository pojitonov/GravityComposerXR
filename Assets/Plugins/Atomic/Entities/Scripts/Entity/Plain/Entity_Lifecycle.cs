using System;
using System.Collections.Generic;
using static Atomic.Entities.AtomicHelper;

namespace Atomic.Entities
{
    public partial class Entity
    {
        private static readonly IEqualityComparer<IEntityUpdate> s_updateComparer =
            EqualityComparer<IEntityUpdate>.Default;

        private static readonly IEqualityComparer<IEntityFixedUpdate> s_fixedUpdateComparer =
            EqualityComparer<IEntityFixedUpdate>.Default;

        private static readonly IEqualityComparer<IEntityLateUpdate> s_lateUpdateComparer =
            EqualityComparer<IEntityLateUpdate>.Default;

        public event Action OnInitialized;
        public event Action OnEnabled;
        public event Action OnDisabled;
        public event Action OnDisposed;

        public event Action<float> OnUpdated;
        public event Action<float> OnFixedUpdated;
        public event Action<float> OnLateUpdated;

        public bool Initialized => this.initialized;
        public bool Enabled => this.enabled;

        private bool initialized;
        private bool enabled;

        private IEntityUpdate[] updates;
        private IEntityFixedUpdate[] fixedUpdates;
        private IEntityLateUpdate[] lateUpdates;

        private int updateCount;
        private int fixedUpdateCount;
        private int lateUpdateCount;

        public void Init()
        {
            if (this.initialized)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityInit initBehaviour)
                    initBehaviour.Init(in this.owner);
            
            this.initialized = true;
            this.OnInitialized?.Invoke();
        }

        public void Dispose()
        {
            if (!this.initialized)
                return;

            if (this.enabled)
                this.Disable();
            
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is IEntityDispose disposeBehaviour)
                    disposeBehaviour.Dispose(in this.owner);

            this.initialized = false;
            this.OnDisposed?.Invoke();
        }

        public void Enable()
        {
            if (!this.initialized)
                this.Init();
            
            if (this.enabled)
                return;
            
            this.enabled = true;

            for (int i = 0; i < _behaviourCount; i++)
                this.EnableBehaviour(in _behaviours[i]);

            this.OnEnabled?.Invoke();
        }

        public void Disable()
        {
            if (!this.enabled)
                return;

            for (int i = 0; i < _behaviourCount; i++)
                this.DisableBehaviour(in _behaviours[i]);

            this.enabled = false;
            this.OnDisabled?.Invoke();
        }

        public void OnUpdate(float deltaTime)
        {
            if (!this.enabled)
                return;

            for (int i = 0; i < this.updateCount && this.enabled; i++)
                this.updates[i].OnUpdate(in this.owner, in deltaTime);

            this.OnUpdated?.Invoke(deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!this.enabled)
                return;

            for (int i = 0; i < this.fixedUpdateCount && this.enabled; i++)
                this.fixedUpdates[i].OnFixedUpdate(in this.owner, in deltaTime);

            this.OnFixedUpdated?.Invoke(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (!this.enabled)
                return;

            for (int i = 0; i < this.lateUpdateCount && this.enabled; i++)
                this.lateUpdates[i].OnLateUpdate(in this.owner, in deltaTime);

            this.OnLateUpdated?.Invoke(deltaTime);
        }

        private void EnableBehaviour(in IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityEnable entityEnable)
                entityEnable.Enable(in this.owner);

            if (behaviour is IEntityUpdate update)
                Add(ref this.updates, ref this.updateCount, in update);

            if (behaviour is IEntityFixedUpdate fixedUpdate)
                Add(ref this.fixedUpdates, ref this.fixedUpdateCount, fixedUpdate);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Add(ref this.lateUpdates, ref this.lateUpdateCount, lateUpdate);
        }

        private void DisableBehaviour(in IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityDisable entityDisable)
                entityDisable.Disable(in this.owner);

            if (behaviour is IEntityUpdate update)
                Remove(ref this.updates, ref this.updateCount, update, s_updateComparer);
            
            if (behaviour is IEntityFixedUpdate fixedUpdate) 
                Remove(ref this.fixedUpdates, ref this.fixedUpdateCount, fixedUpdate, s_fixedUpdateComparer);

            if (behaviour is IEntityLateUpdate lateUpdate)
                Remove(ref this.lateUpdates, ref this.lateUpdateCount, lateUpdate, s_lateUpdateComparer);
        }
    }
}