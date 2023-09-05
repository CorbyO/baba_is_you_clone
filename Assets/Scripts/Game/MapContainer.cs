using System;
using System.Collections;
using System.Collections.Generic;
using Puzzle.Common;
using UnityEngine;

namespace Puzzle.Game
{
    public class MapContainer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int GridSize { get; private set; }
        private EBlockCode[][] _startMap;
        private List<Block>[][] _map;
        private readonly List<Block> _list = new(128);
        public IReadOnlyList<Block> Instances => _list.AsReadOnly();

        public void Initialize(string mapRaw, int gridSize)
        {
            var raw = mapRaw.Replace(" ", "");
            var lines = raw.Split('\n');

            var tempHeight = lines.Length;
            var tempWidth = -1;
            var tempMap = new EBlockCode[tempHeight][];
            var tempInstances = new List<Block>[tempHeight][];

            for (var y = 0; y < tempHeight; y++)
            {
                var line = lines[y];
                var temp = line.Split(',');
                var width = temp.Length;

                if (tempWidth == -1)
                {
                    tempWidth = width;
                }
                else if (width != tempWidth)
                {
                    throw new ArgumentException("올바르지 않는 맵입니다.");
                }

                tempMap[y] = new EBlockCode[tempWidth];
                tempInstances[y] = new List<Block>[tempWidth];

                for (var x = 0; x < tempWidth; x++)
                {
                    if (!int.TryParse(temp[x], out var code))
                    {
                        throw new ArgumentException("올바르지 않는 맵입니다.");
                    }

                    tempMap[y][x] = (EBlockCode)code;
                    tempInstances[y][x] = new(16);
                }
            }

            Width = tempWidth;
            Height = tempHeight;
            _startMap = tempMap;
            _map = tempInstances;
            GridSize = gridSize;
        }

        public IEnumerable<(Vector2Int position, EBlockCode code)> IterateForStart()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    yield return (new(x, y), _startMap[y][x]);
                }
            }
        }

        public IReadOnlyList<Block> Get(int x, int y)
        {
            return _map[y][x].AsReadOnly();
        }

        public IReadOnlyList<Block> Get(Vector2Int pos)
        {
            return Get(pos.x, pos.y);
        }
        
        public bool TryGet(int x, int y, out IReadOnlyList<Block> blocks)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                blocks = null;

                return false;
            }

            blocks = _map[y][x];
            return true;
        }
        
        public bool TryGet(Vector2Int pos, out IReadOnlyList<Block> blocks)
        {
            return TryGet(pos.x, pos.y, out blocks);
        }

        public void Register(int x, int y, Block block)
        {
            block.Position = new(x, y);
            _list.Add(block);
            Add(x, y, block);
        }

        public void Register(Vector2Int pos, Block block)
        {
            Register(pos.x, pos.y, block);
        }

        private void Add(int x, int y, Block block)
        {
            _map[y][x].Add(block);
        }

        private void Add(Vector2Int pos, Block block)
        {
            Add(pos.x, pos.y, block);
        }

        public bool TryMove(Block block, EMoveDirection direction)
        {
            var pos = block.Position;
            var nextPos = pos + direction.ToVector2Int();

            if (nextPos.x < 0 || nextPos.x >= Width || nextPos.y < 0 || nextPos.y >= Height)
            {
                return false;
            }

            Remove(pos, block);
            Add(nextPos, block);

            block.Position = nextPos;

            return true;
        }
        
        public void RemoveAll(int x, int y)
        {
            var list = Get(x, y);

            foreach (var block in _list)
            {
                _list.Remove(block);
            }
            
            _map[y][x].Clear();
        }

        public void RemoveAll(Vector2Int pos)
        {
            RemoveAll(pos.x, pos.y);
        }
        
        public void Remove(int x, int y, Block block)
        {
            //_list.Remove(block);
            _map[y][x].Remove(block);
        }
        
        public void Remove(Vector2Int pos, Block block)
        {
            Remove(pos.x, pos.y, block);
        }
    }
}