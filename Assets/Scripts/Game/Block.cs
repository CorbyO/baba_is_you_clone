using System;
using DG.Tweening;
using Puzzle.Common;
using UnityEngine;

namespace Puzzle.Game
{
    public class Block : MonoBehaviour
    {
        public EBlockCode Code { get; set; }
        public Vector2Int Position { get; set; }

        private Tweener _tweener;
        private Vector3? _pos = null;

        public void Movement(EMoveDirection direction)
        {
            _pos ??= transform.position;
            
            _tweener?.Kill();
            
            var move = direction switch
            {
                EMoveDirection.Left  => Vector3.left,
                EMoveDirection.Right => Vector3.right,
                EMoveDirection.Up    => Vector3.up,
                EMoveDirection.Down  => Vector3.down,
                _                    => throw new ArgumentOutOfRangeException(),
            };

            _pos += move;
            _tweener = transform.DOMove(_pos.Value, 0.1f)
                                .SetEase(Ease.InOutQuad)
                                .SetAutoKill(true)
                                .Play();
        }

        private void OnDestroy()
        {
            _tweener?.Kill();
        }
    }
}