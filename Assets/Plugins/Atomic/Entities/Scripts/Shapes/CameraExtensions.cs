#if ATOMIC && SHAPES_URP
using System;
using UnityEngine;

namespace Atomic.Entities
{
    public static class CameraExtensions
    {
        public static void Draw(this Camera cam, Action drawAction)
        {
            if (!cam || drawAction == null) return;

            using (Shapes.Draw.Command(cam))
            {
                drawAction.Invoke();
            }
        }
    }
}
#endif