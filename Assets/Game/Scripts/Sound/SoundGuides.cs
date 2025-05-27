using UnityEngine;
using Shapes;
using UnityEngine.Rendering;

[ExecuteAlways]
public class SoundGuides : ImmediateModeShapeDrawer
{
    [SerializeField] SoundItem sound;
    
    TextElement text;
    Color color1 = Color.black;
    Color color2 = Color.yellow;
    float thickness = 0.001f;
    private bool collision;

    private void Awake()
    {
        text = new TextElement();
    }

    public override void DrawShapes(Camera cam)
    {
        if (cam == null) return;
        if (!collision) return;

        using (Draw.Command(cam))
        {
            Draw.ZTest = CompareFunction.Always;

            Vector3 origin = transform.position;
            Vector3 direction = Vector3.down;

            if (Physics.Raycast(origin, direction, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Desk"))
                {
                    Draw.Line(origin, hit.point, thickness, color2);
                    Draw.Disc(hit.point, cam.transform.forward, thickness, color1);
                }
            }

            Draw.Disc(origin, cam.transform.forward, thickness, color1);
            var content = "Pitch: " + sound.CurrentPitch.ToString("F1");
            Draw.Text(text, transform.position + new Vector3(0, 0.035f, 0), fontSize: 0.1f, content: content,
                color: color2);
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