using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;
using UnityEngine;

namespace Game.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance { get; private set; }

        private List<EventInstance> eventInstances;
        private List<StudioEventEmitter> eventEmitters;

        private void Awake()
        {
            if (instance)
            {
                Debug.LogWarning("Instance of a AudioManager is already exists");
            }

            instance = this;
            eventInstances = new List<EventInstance>();
            eventEmitters = new List<StudioEventEmitter>();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        public void PlayOneShot(EventReference evt, Vector3 position)
        {
            RuntimeManager.PlayOneShot(evt, position);
        }

        public EventInstance CreateInstance(EventReference evt)
        {
            EventInstance instance = RuntimeManager.CreateInstance(evt);
            eventInstances.Add(instance);
            return instance;
        }

        public StudioEventEmitter InitializeEventEmitter(EventReference evt, GameObject gameObject)
        {
            StudioEventEmitter emitter = gameObject.GetComponent<StudioEventEmitter>();
            emitter.EventReference = evt;
            eventEmitters.Add(emitter);
            return emitter;
        }

        private void CleanUp()
        {
            foreach (EventInstance instance in eventInstances)
            {
                instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                instance.release();
            }

            foreach (StudioEventEmitter emitter in eventEmitters)
            {
                emitter.Stop();
            }
        }
    }
}