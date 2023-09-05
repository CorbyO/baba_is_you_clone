using System;
using UnityEngine;

namespace Puzzle.MapEditor
{
    public abstract class Widget : MonoBehaviour
    {
        protected abstract void Validate();
        public abstract void Clear();

        private void Awake()
        {
            Validate();
        }

        protected void TryThrowNull()
        {
            
        }
    }
}