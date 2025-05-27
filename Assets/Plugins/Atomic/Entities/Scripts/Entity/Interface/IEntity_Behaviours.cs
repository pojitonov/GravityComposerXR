using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IEntity
    {
        event Action<IEntity, IEntityBehaviour> OnBehaviourAdded;
        event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted;

        int BehaviourCount { get; }

        void AddBehaviour(in IEntityBehaviour behaviour);
        T GetBehaviour<T>() where T : IEntityBehaviour;
        bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour;

        bool HasBehaviour(in IEntityBehaviour behaviour);
        bool HasBehaviour<T>() where T : IEntityBehaviour;
        
        bool DelBehaviour(in IEntityBehaviour behaviour);
        bool DelBehaviour<T>() where T : IEntityBehaviour;

        void ClearBehaviours();

        IEntityBehaviour[] GetBehaviours();
        int GetBehaviours(in IEntityBehaviour[] results);
        IEnumerator<IEntityBehaviour> BehaviourEnumerator();
    }
}