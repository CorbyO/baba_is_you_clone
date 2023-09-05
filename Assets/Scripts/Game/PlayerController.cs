using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Puzzle.Common;

namespace Puzzle.Game
{
    public sealed class PlayerController
    {
        public event Action OnMove;

        public bool _isMoving = false;

        public void Initialize() { }

        public void Movement(EMoveDirection direction)
        {
            if (_isMoving)
            {
                return;
            }

            _isMoving = true;

            var map = GameManager.Instance.MapContainer;
            var mapReader = GameManager.Instance.MapReader;
            var force = direction.ToVector2Int();

            foreach (var block in map.Instances)
            {
                if (block.Code != EBlockCode.Player)
                {
                    continue;
                }

                // 밀수있는 블럭까지의 거리
                var nextPos = block.Position;
                
                var canMove = false;

                while (true)
                {
                    nextPos += force;
                    
                    // 다음이 벽인가?
                    if (!map.TryGet(nextPos, out var nextBlocks))
                    {
                        canMove = false;
                        nextPos -= force;
                        
                        goto EndPoint;
                    }

                    // 텅 비어있으면 종료
                    if (nextBlocks.Count == 0)
                    {
                        canMove = true;
                        nextPos -= force;

                        goto EndPoint;
                    }

                    var hasPush = false;
                    foreach (var nextBlock in nextBlocks)
                    {
                        if (mapReader.HasTag(nextBlock.Code, EBlockCode.PStop))
                        {
                            if (mapReader.HasTag(nextBlock.Code, EBlockCode.PPush))
                            {
                                hasPush = true;
                                canMove = true;
                            }
                            else
                            {
                                canMove = false;
                                nextPos -= force;
                                
                                goto EndPoint;
                            }
                        }
                        else if (mapReader.HasTag(nextBlock.Code, EBlockCode.PPush))
                        {
                            hasPush = true;
                            canMove = true;
                        }
                    }
                    
                    if (!hasPush)
                    {
                        canMove = true;
                        nextPos -= force;
                        
                        goto EndPoint;
                    }
                }

                EndPoint:

                if (!canMove)
                {
                    continue;
                }

                // 밀기
                List<Block> pushedBlocks = new(16);
                for (var pos = nextPos; pos != block.Position; pos -= force)
                {
                    foreach (var pushed in map.Get(pos))
                    {
                        if (!mapReader.HasTag(pushed.Code, EBlockCode.PPush))
                        {
                            continue;
                        }
                        
                        pushedBlocks.Add(pushed);
                    }
                }
                
                foreach (var pushed in pushedBlocks)
                {
                    Movement(pushed, direction);
                }
                
                // 자신 이동
                Movement(block, direction);
            }

            _isMoving = true;
            UniTask.Delay(100).ContinueWith(() =>
            {
                _isMoving = false;
                OnMove?.Invoke();
            }).Forget();
        }

        private void Movement(Block block, EMoveDirection direction)
        {
            var map = GameManager.Instance.MapContainer;

            if (!map.TryMove(block, direction))
            {
                throw new();
            }
            block.Movement(direction);
        }
    }
}