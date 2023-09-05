using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Puzzle.Common;
using UnityEngine;

namespace Puzzle.Game
{
    public class MapController
    {
        public void Initialize()
        {
        }
        
        public async UniTask Generate(IBlockFactory blockFactory)
        {
            var map = GameManager.Instance.MapContainer;
            
            foreach (var (position, code) in map.IterateForStart())
            {
                if (code == EBlockCode.Empty)
                {
                    continue;
                }
                
                var pos = position;
                pos.y = map.Height - pos.y - 1;

                var block = await blockFactory.Create(pos, map.GridSize, code);
                map.Register(pos, block);
            }
        }

        public bool IsWin()
        {
            var map = GameManager.Instance.MapContainer;
            var mapReader = GameManager.Instance.MapReader;
            
            foreach (var block in map.Instances)
            {
                if (block.Code != EBlockCode.Player)
                {
                    continue;
                }

                Debug.Log("Player: ");
                foreach (var t in map.Get(block.Position))
                {
                    Debug.Log(t.Code);
                }

                foreach (var friend in map.Get(block.Position))
                {
                    if (!friend.Code.IsObject())
                    {
                        continue;
                    }

                    if (mapReader.HasTag(friend.Code, EBlockCode.PWin))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}