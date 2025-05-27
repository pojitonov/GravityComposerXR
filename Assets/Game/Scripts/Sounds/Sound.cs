using UnityEngine;
using UnityEngine.Rendering;
using FMOD.Studio;
using FMODUnity;
using PrimeTween;
using Shapes;

public class Sound : ImmediateModeShapeDrawer
{
    public EventInstance EventInstance => instance;
    public bool IsPlaying => isPlaying;
    
    [SerializeField] private EventReference sound;
    
    private float radius = 0.02f;
    private float thickness = 0.001f;
    private Color color = Color.black;

    private TextElement text;
    private Tween tween;
    private EventInstance instance;
    
    private bool isPlaying;
    private bool isTriggered;
    private float fadeDuration = 5f;
    private float animatedVal;

    
    private void Awake()
    {
        text = new TextElement();
        instance = AudioManager.instance.CreateInstance(sound);
        instance.setParameterByName("Master", 0f);
        instance.setParameterByName("Volume", 1f);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;
        
        isTriggered = true;
        FadeIn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;
        
        isTriggered = false;
        FadeOut();
    }
    
    
    private void FadeIn()
    {
        if (!instance.isValid()) return;
        
        instance.set3DAttributes(transform.position.To3DAttributes());
        instance.setParameterByName("Master", 0);
        instance.start();

        tween.Stop();
        tween = Tween.Custom(0f, 1f, fadeDuration,
            onValueChange: val =>
            {
                instance.setParameterByName("Master", val);
                animatedVal = val * radius;
            })
            .OnComplete(() =>
        {
            isPlaying = true;
        });
    }


    private void FadeOut()
    {
        tween.Stop();
        instance.getParameterByName("Master", out var current);
        tween = Tween.Custom(current, 0f, fadeDuration,
                onValueChange: val =>
                {
                    instance.setParameterByName("Master", val);
                    animatedVal = val * radius;
                })
            .OnComplete(() =>
            {
                instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                isPlaying = false;
            });
    }

    public override void DrawShapes(Camera cam)
    {
        if (!cam) return;
        using (Draw.Command(cam))
        {
            Draw.ZTest = CompareFunction.Always;
            Draw.Sphere(transform.position, animatedVal, color);
            var content = "Sine Wave 100Hz";
            Draw.Text(text, transform.position + new Vector3(0, 0.035f, 0), fontSize: 0.1f, content: content,
                color: color);
        }
    }
}