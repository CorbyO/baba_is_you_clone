using System;
using Puzzle.Utils;
using UnityEngine;

namespace Puzzle.MapEditor
{
    public class Lobby : Widget
    {
        public event Action OnRequestNew;
        public event Action OnRequestLoad;
        
        protected override void Validate()
        {
        }

        public override void Clear()
        {
            
        }
        
        public void RequestNew()
        {
            OnRequestNew?.Invoke();
        }
        
        public void RequestLoad()
        {
            OnRequestLoad?.Invoke();
        }
        public void Exit()
        {
            App.Quit();
        }
    }
}