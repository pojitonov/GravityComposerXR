using UnityEngine;

namespace Atomic.Entities
{
    public interface IEntityShapes : IEntityBehaviour
    {
        void OnShapesDraw(Camera cam, in IEntity entity);
    }

    public interface IEntityShapes<in T> : IEntityShapes where T : IEntity
    {
        void IEntityShapes.OnShapesDraw(Camera cam, in IEntity entity) => this.OnShapesDraw(cam, (T)entity);
        void OnShapesDraw(Camera cam, T entity);
    }
}