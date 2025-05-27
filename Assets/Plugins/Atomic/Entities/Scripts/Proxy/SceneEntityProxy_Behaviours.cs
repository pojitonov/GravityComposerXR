using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntityProxy<E>
    {
        public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded
        {
            add => _source.OnBehaviourAdded += value;
            remove => _source.OnBehaviourAdded -= value;
        }

        public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted
        {
            add => _source.OnBehaviourDeleted += value;
            remove => _source.OnBehaviourDeleted -= value;
        }

        public int BehaviourCount => _source.BehaviourCount;

        public void AddBehaviour(in IEntityBehaviour behaviour) => _source.AddBehaviour(in behaviour);

        public T GetBehaviour<T>() where T : IEntityBehaviour => _source.GetBehaviour<T>();
        public bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour => _source.TryGetBehaviour(out behaviour);

        public bool HasBehaviour<T>() where T : IEntityBehaviour => _source.HasBehaviour<T>();
        public bool HasBehaviour(in IEntityBehaviour behaviour) => _source.HasBehaviour(in behaviour);

        public bool DelBehaviour(in IEntityBehaviour behaviour) => _source.DelBehaviour(in behaviour);
        public bool DelBehaviour<T>() where T : IEntityBehaviour => _source.DelBehaviour<T>();

        public void ClearBehaviours() => _source.ClearBehaviours();
        
        public int GetBehaviours(in IEntityBehaviour[] results) => _source.GetBehaviours(in results);
        public IEntityBehaviour[] GetBehaviours() => _source.GetBehaviours();
        public IEnumerator<IEntityBehaviour> BehaviourEnumerator() => _source.BehaviourEnumerator();
    }
}