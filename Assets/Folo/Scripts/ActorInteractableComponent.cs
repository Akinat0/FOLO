using UnityEngine;

namespace Folo.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Actor))]
    public class ActorInteractableComponent : MonoBehaviour
    {
        [SerializeField] private Actor actor;

        public Actor Actor => actor;

        private Collider2D Collider { get; set; }
        void Awake()
        {
            actor = actor ? actor : GetComponent<Actor>();
            Collider = GetComponent<Collider2D>();
            Collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Interactor interactor))
                return;
            
            interactor.AddIntractable(this);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Interactor interactor))
                return;
            
            interactor.RemoveInteractable(this);
        }

        public void Interact(Actor interactor)
        {
            actor.SendInteractEvent(interactor);
        }
    }
}