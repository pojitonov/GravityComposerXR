using FMOD.Studio;
using FMODUnity;
using Game.Audio;
using PrimeTween;
using UnityEngine;

public class EffectItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;
    }
}