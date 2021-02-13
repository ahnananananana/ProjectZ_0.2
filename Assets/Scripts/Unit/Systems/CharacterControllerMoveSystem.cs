using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMoveSystem : MoveSystem
    {
        private CharacterController characterController;

        protected override void OnAwake()
        {
            characterController = GetComponent<CharacterController>();
        }

        protected override void OnMove(Vector3 direction, float speed)
        {
            characterController.SimpleMove(direction * speed);
        }
    }
}
