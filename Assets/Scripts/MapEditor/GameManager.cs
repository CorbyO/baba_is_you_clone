using System;
using System.Collections;
using System.Collections.Generic;
using Puzzle.Utils;
using UnityEngine;

namespace Puzzle.MapEditor
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;
        
        [Header("Prefabs")]
        [SerializeField]
        private Lobby _lobby;
        [SerializeField]
        private MapNameInputter _mapNameInputter;
        
        
        public void Awake()
        {
            Instantiate();
            EventHandling();
        }

        private void Instantiate()
        {
            Instantiate(ref _lobby, true);
            Instantiate(ref _mapNameInputter, false);
        }

        private void EventHandling()
        {
            _lobby.OnRequestNew += () =>
            {
                _lobby.gameObject.SetActive(false);
                _mapNameInputter.Clear();
                _mapNameInputter.gameObject.SetActive(true);
            };
            
            _mapNameInputter.OnCancel += () =>
            {
                _mapNameInputter.gameObject.SetActive(false);
                _lobby.gameObject.SetActive(true);
            };
        }

        private void Instantiate<T>(ref T widget, bool isEnabled)
            where T : Widget
        {
            widget.TryThrowNull();
            widget = Instantiate(widget, _canvas.transform);
            widget.gameObject.SetActive(isEnabled);
        }
    }

}