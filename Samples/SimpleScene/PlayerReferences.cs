using UnityEngine;

namespace PMR.Samples
{
    public class PlayerReferences : GameObjectReferences<PlayerReferences>
    {
        public CharacterController CharacterController { get; private set; }
        public Health Health { get; private set; }

        // Do your GetComponent and AddComponent calls in this method.
        protected override void PrepareComponents()
        {
            CharacterController = gameObject.AddComponent<CharacterController>();
            Health = GetComponent<Health>();
        }

        // Here you can register all the non-monobehaviour types for this object
        protected override void RegisterPocoTypes()
        {
            // Player movement will be created on this object.
            RegisterPocoType<PlayerMovement>();
        }
    }
}