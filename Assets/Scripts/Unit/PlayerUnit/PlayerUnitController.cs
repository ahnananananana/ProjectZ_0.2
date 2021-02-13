using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class PlayerUnitController : UnitController
    {
        private PlayerUnitStateModel Model => model as PlayerUnitStateModel;

        private InputSystem inputSystem;
        [SerializeField] private FollowingCamera followingCamera;
        private MoveSystem moveSystem;

        private void Awake()
        {
            model = GetComponent<PlayerUnitStateModel>();

            followingCamera.SetTarget(transform);

            inputSystem = GetComponent<InputSystem>();
            moveSystem = GetComponent<MoveSystem>();

            //inputSystem.MoveInputEvent += Model.MoveInput.SetValue;
        }

        private void OnMoveInput(Vector2 input)
        {
        }

    }
}
