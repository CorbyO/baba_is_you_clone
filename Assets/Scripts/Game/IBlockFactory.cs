using Cysharp.Threading.Tasks;
using Puzzle.Common;
using UnityEngine;

namespace Puzzle.Game
{
    public interface IBlockFactory
    {
        public UniTask<Block> Create(Vector2Int pos, int gridSize, EBlockCode code);
    }
}