using Atomic.Elements;
using Atomic.Entities;
using Prototype;
using Shapes;
using UnityEngine;

public class DrawGuidesBehaviour : IEntityInit
{
    private TriggerEventReceiver triggerReceiver;
    TextElement text;
    Color color1 = Color.black;
    Color color2 = Color.yellow;
    float thickness = 0.001f;
    private bool trigger;

    public DrawGuidesBehaviour(TriggerEventReceiver triggerReceiver)
    {
        this.triggerReceiver = triggerReceiver;
    }

    public void Init(in IEntity entity)
    {
        text = new TextElement();
        triggerReceiver.OnEntered += OnTriggerEnter;
        triggerReceiver.OnExited += OnTriggerExit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;

        Debug.Log("OnTriggerEnter");
        trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ring"))
            return;

        Debug.Log("OnTriggerExit");
        trigger = false;
    }
}