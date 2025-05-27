using UnityEngine;
using Shapes;

[ExecuteAlways]
public class DrawAudiowave : ImmediateModeShapeDrawer
{
    public AudioSource audioSource;
    float[] samples = new float[256];

    public override void DrawShapes(Camera cam)
    {
        audioSource.GetOutputData(samples, 0);

        Draw.Color = Color.yellow;
        Draw.Thickness = 0.05f;

        PolylinePath path = new PolylinePath();

        for (int i = 0; i < samples.Length; i++)
        {
            float x = (float)i / samples.Length * 10f - 5f;
            float y = samples[i] * 2f;
            path.AddPoint(new Vector3(x, y, 0f));
        }

        Draw.Polyline(path, PolylineJoins.Simple);
    }
}