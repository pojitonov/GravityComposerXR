using UnityEngine;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public static class ShapesDrawSystem
    {
        private static readonly List<IEntity> entities = new();

        public static void Register(IEntity entity)
        {
            if (!entities.Contains(entity))
                entities.Add(entity);
        }

        public static void Unregister(IEntity entity)
        {
            entities.Remove(entity);
        }

        public static void DrawAllShapes(Camera cam)
        {
            foreach (var entity in entities)
            {
                var behaviours = entity.GetBehaviours();
                foreach (var behaviour in behaviours)
                {
                    if (behaviour is IEntityShapes shapeDrawer)
                        shapeDrawer.OnShapesDraw(cam, entity);
                }
            }
        }
    }
}