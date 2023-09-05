using System;
using System.Collections.Generic;
using Puzzle.Common;
using UnityEngine;

namespace Puzzle.Game
{
    public class MapReader
    {
        private readonly Dictionary<EBlockCode, List<EBlockCode>> _tags = new();
        public IReadOnlyDictionary<EBlockCode, List<EBlockCode>> Tags => _tags;

        public void Initialize()
        {
            var array = Enum.GetValues(typeof(EBlockCode));

            foreach (EBlockCode code in array)
            {
                _tags[code] = new(16);

                if (!code.IsObject())
                {
                    _tags[code].Add(EBlockCode.PPush);
                    _tags[code].Add(EBlockCode.PStop);
                }
            }
        }

        public void Read()
        {
            RemoveTags();

            Debug.Log($"Start reading map...");

            var map = GameManager.Instance.MapContainer;

            foreach (var block in map.Instances)
            {
                if (!block.Code.IsSubject())
                {
                    continue;
                }

                var pos = block.Position;

                ReadToRight(block, pos);
                ReadToDown(block, pos);
            }
        }

        public void ReadToRight(Block block, Vector2Int pos)
        {
            Read(block, pos, Vector2Int.right);
        }
        
        public void ReadToDown(Block block, Vector2Int pos)
        {
            Read(block, pos, Vector2Int.down);
        }

        public void Read(Block block, Vector2Int pos, Vector2Int direction)
        {
            var map = GameManager.Instance.MapContainer;
            
            pos += direction;
            if (!map.TryGet(pos, out var @is))
            {
                return;
            }

            if (@is.Count != 1)
            {
                return;
            }

            if (@is[0].Code != EBlockCode.Is)
            {
                return;
            }
            pos += direction;
            if (!map.TryGet(pos, out var purpose))
            {
                return;
            }

            if (purpose.Count != 1)
            {
                return;
            }

            if (!purpose[0].Code.IsPurpose())
            {
                return;
            }

            var obj = block.Code.ToObject();
            _tags[obj].Add(purpose[0].Code);

            Debug.Log($"{obj} U {purpose[0].Code}");
        }

        public bool HasTag(EBlockCode anyone, EBlockCode purpose)
        {
            return _tags[anyone].Contains(purpose);
        }

        public void RemoveTags()
        {
            foreach (var tag in _tags)
            {
                if (tag.Key.IsObject())
                {
                    tag.Value.Clear();
                }
            }
        }
    }
}