using System;
using UnityEngine;
using UnityEngine.Rendering;
using FMOD.Studio;
using FMODUnity;
using PrimeTween;
using Shapes;

public class SoundFX : ImmediateModeShapeDrawer
{
    [SerializeField] private string parameterName;

    private Color color = Color.black;
    private Color color2 = Color.yellow;
    private float thickness = 0.0025f;

    private TextElement text;
    private Tween tween;
    private EventInstance instance;
    private Sound sound;
    private Bounds bounds;

    private bool isTriggered;
    private bool isSoundFound;

    private float currentVal;
    private float currentTremolo;

    private void Awake()
    {
        text = new TextElement();
    }

    private void Update()
    {
        GetSound();
        UpdateParam();
    }

    private void GetSound()
    {
        if (!isTriggered) return;

        sound = FindAnyObjectByType<Sound>();
        isSoundFound = sound;

        if (!isSoundFound) return;
        instance = sound.EventInstance;
    }

    public override void DrawShapes(Camera cam)
    {
        if (!cam) return;
        
        var content = string.Empty;
        if (!isTriggered)
        {
            content = $"{parameterName}";
        }
        else
        {
            content = $"{parameterName}: " + currentVal.ToString("F1");
        }

        using (Draw.Command(cam))
        {
            Draw.ZTest = CompareFunction.Always;

            Vector3 origin = transform.position;
            Vector3 direction = Vector3.down;
            
            Draw.Text(text, transform.position + new Vector3(0, 0.035f, 0), fontSize: 0.1f, content: content,
                color: color2);

            if (!isTriggered) return;
            LayerMask deskMask = LayerMask.GetMask("TransparentFX");

            if (Physics.Raycast(origin, direction, out RaycastHit hit, Mathf.Infinity, deskMask))
            {
                if (hit.collider.CompareTag("Desk"))
                {
                    Draw.Line(origin, hit.point, thickness, color2);
                    Draw.Disc(hit.point, cam.transform.forward, thickness, Color.black);
                }
            }

            Draw.Disc(origin, cam.transform.forward, thickness, color);
        }
    }

    private void UpdateParam()
    {
        if (!isSoundFound) return;

        var triggerMin = bounds.min.y;
        var triggerMax = bounds.max.y;
        var objectY = transform.position.y;
        var normalized = Mathf.InverseLerp(triggerMin, triggerMax, objectY);
        var remapped = Mathf.Lerp(-1f, 1f, normalized);

        if (Mathf.Abs(remapped - currentVal) > 0.1f)
        {
            Tween.Custom(currentVal, remapped, 0.5f,
                onValueChange: val =>
                {
                    currentVal = val;
                    instance.setParameterByName(parameterName, val);
                });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;

        bounds = other.bounds;
        instance.setParameterByName(parameterName, currentVal);
        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;

        instance.setParameterByName(parameterName, 0);
        isTriggered = false;
    }
}