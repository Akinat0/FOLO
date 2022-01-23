
using Bolt;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class Actor : MonoBehaviour
{
    [SerializeField] string actorName;

    public string ActorName => !string.IsNullOrEmpty(actorName) ? actorName : gameObject.name;

    public Vector3 Position => transform.position;

    public void SendTriggerEnterEvent(Actor other)
    {
        SendCustomEvent($"{other.ActorName}_Enter", other);
    }
    
    public void SendTriggerStayEvent(Actor other)
    {
        SendCustomEvent($"{other.ActorName}_Stay", other);
    }
    
    public void SendTriggerExitEvent(Actor other)
    {
        SendCustomEvent($"{other.ActorName}_Exit", other);
    }

    public void SendInteractEvent(Actor actor)
    {
        SendCustomEvent("Interact", actor);
    }
    
    public void SendCustomEvent(string eventName, params object[] args)
    {
        Debug.Log($"Trigger event {eventName} on {ActorName}");
        CustomEvent.Trigger(gameObject, eventName, args);
    }
}
