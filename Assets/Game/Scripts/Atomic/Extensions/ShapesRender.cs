using UnityEngine;

public class ShapesRenderHook : MonoBehaviour
{
    void OnPostRender()
    {
        var cam = Camera.current;
        if (cam == null) return;

        Atomic.Entities.ShapesDrawSystem.DrawAllShapes(cam);
    }
}