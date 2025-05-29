using System.Collections.Generic;
using Flexalon;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using PrimeTween;
using Shapes;

public class Ring : ImmediateModeShapeDrawer
{
    [SerializeField] private EventReference sound;

    private Color color = Color.white;
    private Color color2 = Color.yellow;
    private float waveLength = 1f;
    private float waveAmplitude = 0.1f;
    private int pointsCount = 50;
    private float waveSpeed = 2f;

    private TextElement text;
    private Tween tween;
    private EventInstance instance;
    private bool isTriggered;

    private HashSet<Collider> activeColliders = new();
    private Vector3 TextOffset() => new(0, 0.22f, 0);

    private void Awake()
    {
        text = new TextElement();
        instance = AudioManager.instance.CreateInstance(sound);
        instance.setParameterByName("Master", 0.35f);

        PlaySound();
    }

    private void PlaySound()
    {
        instance.set3DAttributes(transform.position.To3DAttributes());
        instance.start();
    }


    public override void DrawShapes(Camera cam)
    {
        if (!cam) return;
        if (isTriggered) return;
        using (Draw.Command(cam))
        {
            var content = "Place Objects to Generate Sound";
            Draw.Text(text, transform.position + TextOffset(), fontSize: 0.15f, content: content,
                color: color);
            DrawSineWave();
        }
    }

    private void DrawSineWave()
    {
        var center = transform.position + TextOffset();
        float waveLength = 0.25f;
        float amplitude = 0.025f;
        float speed = 1f;
        int segments = 50;

        Vector3 start = center - new Vector3(waveLength / 2f, 0, 0);
        Vector3 prev = start;

        float time = Time.time * speed;

        for (int i = 1; i <= segments; i++)
        {
            float t = (float)i / segments;
            float x = t * waveLength;

            float noiseInputX = t * 20f + time;
            float y = (Mathf.PerlinNoise(noiseInputX, 0f) - 0.0f) * 2f * amplitude;

            Vector3 current = start + new Vector3(x, y, 0);
            Draw.Line(prev, current, 0.002f, color2);
            prev = current;
        }
    }
}