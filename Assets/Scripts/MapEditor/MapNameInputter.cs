using System;
using Puzzle.Utils;
using TMPro;
using UnityEngine;

namespace Puzzle.MapEditor
{
    public class MapNameInputter : Widget
    {
        [SerializeField]
        private TMP_InputField _inputField;
        private string _mapName;
        
        public event Action<string> OnOk;
        public event Action OnCancel;
        
        protected override void Validate()
        {
            _inputField.TryThrowNull();
        }

        public override void Clear()
        {
            _inputField.text = "";
            _mapName = "";
        }

        public void SetName(string newName)
        {
            _mapName = newName;
        }

        public void Ok()
        {
            if (_mapName.IsNullOrEmpty())
            {
                return;
            }
            
            OnOk?.Invoke(_mapName);
        }
        
        public void Cancel()
        {
            OnCancel?.Invoke();
        }
    }
}