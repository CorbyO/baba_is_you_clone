using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Puzzle.Common;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Puzzle.Game
{
    public sealed class BlockFactory : IBlockFactory
    {
        public IReadOnlyDictionary<EBlockCode, string> BlockPrefabs { get; internal set; }

        public async UniTask<Block> Create(Vector2Int pos, int gridSize, EBlockCode code)
        {
            var instance = await Addressables.InstantiateAsync($"Blocks/{code.ToString()}").ToUniTask();

            if (!instance.TryGetComponent<Block>(out var block))
            {
                block = instance.AddComponent<Block>();
            }
            
            block.transform.position = new(pos.x * gridSize, pos.y * gridSize);
            block.Code = code;
            
            return block;
        }
    }
}