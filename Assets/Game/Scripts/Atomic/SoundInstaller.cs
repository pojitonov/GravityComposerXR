using Atomic.Elements;
using Atomic.Entities;
using FMODUnity;
using Prototype;
using UnityEngine;

namespace Game.Gameplay
{
    public class SoundInstaller : SceneEntityInstaller
    {
        [Header("Main")]
        [SerializeField] private EventReference sound;
        [SerializeField] private TriggerEventReceiver triggerReceiver;


        public override void Install(IEntity entity)
        {
            entity.AddTransform(transform);
            entity.AddSound(sound);
            entity.AddTriggerReceiver(triggerReceiver);
            entity.AddBehaviour(new DrawGuidesBehaviour(triggerReceiver));
        }
    }
}