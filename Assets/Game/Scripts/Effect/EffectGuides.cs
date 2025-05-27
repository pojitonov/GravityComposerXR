using UnityEngine;
using Shapes;
using UnityEngine.Rendering;

[ExecuteAlways]
public class EffectGuides : ImmediateModeShapeDrawer
{
    [SerializeField] EffectItem effect;

    private bool collision;

    public override void DrawShapes(Camera cam)
    {
        if (cam == null) return;
        if (!collision) return;

        using (Draw.Command(cam))
        {
            Draw.ZTest = CompareFunction.Always;
            Vector3 origin = transform.position;
            float thickness = 0.001f;
            float radius = 0.05f;
            Color color1 = Color.yellow;
            
            Draw.Ring(origin, transform.up, radius: radius, thickness: thickness, color1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;

        collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;

        collision = false;
    }
}