using System;
using UnityEngine;

namespace Puzzle.Game
{
    public class CameraController : MonoBehaviour
    {
        private Camera _camera;

        protected Camera Camera
        {
            get
            {
                _camera ??= GetComponent<Camera>();

                return _camera;
            }
        }
        
        public void Initialize()
        {
        }

        public void MatchWithMap()
        {
            var map = GameManager.Instance.MapContainer;
            
            float width = map.Width - 1;
            float height = map.Height - 1;
            
            var center = new Vector3(width * 0.5f, height * 0.5f, -10);
            var displayRatio = (float)Screen.width / Screen.height;
            var size = displayRatio > 1 ? map.Height : map.Width / displayRatio;

            transform.position = center * map.GridSize;
            Camera.orthographicSize = size * 0.5f * map.GridSize;
        }
    }
}