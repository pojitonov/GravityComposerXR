using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded
        {
            add => this.Entity.OnBehaviourAdded += value;
            remove => this.Entity.OnBehaviourAdded -= value;
        }

        public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted
        {
            add => this.Entity.OnBehaviourDeleted += value;
            remove => this.Entity.OnBehaviourDeleted -= value;
        }
        
        public int BehaviourCount => Entity.BehaviourCount;

        public void AddBehaviour(in IEntityBehaviour behaviour) => this.Entity.AddBehaviour(in behaviour);

        public T GetBehaviour<T>() where T : IEntityBehaviour => Entity.GetBehaviour<T>();
        public bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour => Entity.TryGetBehaviour(out behaviour);

        public IEntityBehaviour[] GetBehaviours() => Entity.GetBehaviours();
        public int GetBehaviours(in IEntityBehaviour[] results) => Entity.GetBehaviours(in results);
        public IEntityBehaviour GetBehaviourAt(in int index) => this.Entity.GetBehaviourAt(index);
        
        public bool DelBehaviour(in IEntityBehaviour behaviour) => this.Entity.DelBehaviour(in behaviour);
        public bool DelBehaviour<T>() where T : IEntityBehaviour => Entity.DelBehaviour<T>();

        public bool HasBehaviour<T>() where T : IEntityBehaviour => Entity.HasBehaviour<T>();
        public bool HasBehaviour(in IEntityBehaviour behaviour) => this.Entity.HasBehaviour(in behaviour);
        public void ClearBehaviours() => this.Entity.ClearBehaviours();
        
        public IEnumerator<IEntityBehaviour> BehaviourEnumerator() => Entity.BehaviourEnumerator();
    }
}