using System;
using Puzzle.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Puzzle.Game
{
    public class InputController : MonoBehaviour
    {
        private const float TOLERANCE = 0.001f;
        public event Action<EMoveDirection> OnMovement;

        private Vector2 _prevMoveInput = Vector2.zero;
        
        public void Initialize()
        {
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var current = context.ReadValue<Vector2>();
            var prev = _prevMoveInput;

            _prevMoveInput = current;
            
            if (!Mathf.Approximately(current.x, prev.x))
            {
                switch (current.x)
                {
                    case > 0:
                        OnMovement?.Invoke(EMoveDirection.Right);

                        return;
                    case < 0:
                        OnMovement?.Invoke(EMoveDirection.Left);

                        return;
                }
            }

            if (!Mathf.Approximately(current.y, prev.y))
            {
                switch (current.y)
                {
                    case > 0:
                        OnMovement?.Invoke(EMoveDirection.Up);

                        return;
                    case < 0:
                        OnMovement?.Invoke(EMoveDirection.Down);

                        return;
                }
            }
        }
    }
}