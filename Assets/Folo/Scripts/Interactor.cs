using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Folo.Scripts
{
    [RequireComponent(typeof(Actor))]
    public class Interactor : MonoBehaviour
    {
        public Actor Actor { get; private set; }

        void Awake()
        {
            Actor = Actor ? Actor : GetComponent<Actor>();
        }
        
        private readonly HashSet<ActorInteractableComponent> interactables = new HashSet<ActorInteractableComponent>(); 

        public void AddIntractable(ActorInteractableComponent interactable)
        {
            interactables.Add(interactable);
        }
        
        public void RemoveInteractable(ActorInteractableComponent interactable)
        {
            interactables.Remove(interactable);
        }

        public bool Interact()
        {
            if (interactables.Count == 0)
                return false;

            if (interactables.Count == 1)
            {
                interactables.First().Interact(Actor);
                return true;
            }

            float SqrDistance(ActorInteractableComponent interactable)
                => Vector3.SqrMagnitude(interactable.Actor.Position - Actor.Position);

            interactables.OrderBy(SqrDistance).First().Interact(Actor);
            return true;
        }
    }
}