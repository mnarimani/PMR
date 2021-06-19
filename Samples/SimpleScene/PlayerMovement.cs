using UnityEngine;

namespace PMR.Samples
{
    public class PlayerMovement : ITickable, IFixedTickable, IRequire<PlayerReferences>
    {
        private CharacterController _characterController;
        private Health _health;
        private float _h;
        private float _v;

        public void Init(PlayerReferences dep)
        {
            _characterController = dep.CharacterController;
            _health = dep.Health;
        }

        public void Tick(float deltaTime)
        {
            if (_health.CurrentHealth <= 0)
                return;
            
            _h = Input.GetAxis("Horizontal");
            _v = Input.GetAxis("Vertical");
        }

        public void FixedTick(float fixedDeltaTime)
        {
            if (_health.CurrentHealth <= 0)
                return;
            
            _characterController.SimpleMove(new Vector3(_h, 0, _v) * 6);
        }
    }
}