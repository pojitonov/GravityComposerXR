using System;
using FMOD.Studio;
using FMODUnity;
using Game.Audio;
using PrimeTween;
using UnityEngine;

public class SoundItem : MonoBehaviour
{
    public float CurrentPitch => currentPitch;

    [SerializeField] private EventReference sound;

    private float fadeDuration = 1f;
    private EventInstance instance;
    private bool isPlaying;
    private Tween tween;

    private float triggerMinY;
    private float triggerMaxY;
    private Transform @object;
    private Bounds bounds;
    private float currentPitch;

    private void Awake()
    {
        instance = AudioManager.instance.CreateInstance(sound);
    }

    private void Update()
    {
        if (!isPlaying) return;

        float triggerMinY = bounds.min.y;
        float triggerMaxY = bounds.max.y;

        float objectY = transform.position.y;

        float normalized = Mathf.InverseLerp(triggerMinY, triggerMaxY, objectY);
        float remapped = Mathf.Lerp(-1f, 1f, normalized);
        if (Mathf.Abs(remapped - currentPitch) > 0.1f)
        {
            Tween.Custom(currentPitch, remapped, 0.5f,
                onValueChange: val =>
                {
                    currentPitch = val;
                    instance.setParameterByName("Pitch", val);
                });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ring")) return;
        if (!instance.isValid()) return;


        instance.set3DAttributes(transform.position.To3DAttributes());
        instance.start();
        instance.setParameterByName("Volume", 0);

        tween.Stop();
        tween = Tween.Custom(0f, 1f, fadeDuration,
            onValueChange: val => { instance.setParameterByName("Volume", val); });

        isPlaying = true;
        bounds = other.bounds;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ring")) return;

        tween.Stop();
        instance.getParameterByName("Volume", out var result);
        tween = Tween.Custom(result, 0f, fadeDuration,
                onValueChange: val => { instance.setParameterByName("Volume", val); })
            .OnComplete(() =>
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
                isPlaying = false;
            });
    }
}