using UnityEngine;

namespace Folo.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Actor))]
    public class ActorTriggerComponent : MonoBehaviour
    {
        [SerializeField] private Actor actor;
        private Collider2D Collider { get; set; }
        void Awake()
        {
            actor = actor ? actor : GetComponent<Actor>();
            Collider = GetComponent<Collider2D>();
            Collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Actor otherActor))
                actor.SendTriggerEnterEvent(otherActor);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Actor otherActor))
                actor.SendTriggerStayEvent(otherActor);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Actor otherActor))
                actor.SendTriggerExitEvent(otherActor);
        }
    }
}
