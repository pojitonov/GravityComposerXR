using FMODUnity;
using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    [SerializeField] private EventReference sound;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ring")) return;
        AudioManager.instance.PlayOneShot(sound, this.transform.position);
    }
}
