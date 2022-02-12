using Bolt;
using UnityEngine;

namespace Folo.Scripts
{
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

        public void SendInteractEvent(Actor interactor)
        {
            SendCustomEvent("Interact", interactor);
        }
    
        public void SendSetInteractableEvent(bool interactable)
        {
            SendCustomEvent("SetInteractable", interactable);
        }
    
        public void SendCustomEvent(string eventName, params object[] args)
        {
            Debug.Log($"Trigger event {eventName} on {ActorName}");
            CustomEvent.Trigger(gameObject, eventName, args);
        }
    }
}
