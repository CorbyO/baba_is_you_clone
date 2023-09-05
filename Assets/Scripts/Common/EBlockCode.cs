using System;
using UnityEngine;

namespace Puzzle.Common
{
    public enum EBlockCode
    {
        Empty = 0,
        Player,
        Wall,
        Flag,
        Rock,
        
        Is = 100,
        And,
        
        PWin = 200,
        PPush,
        PStop,
        
        SEmpty = 300,
        SPlayer,
        SWall,
        SFlag,
        SRock,
    }
    
    public static class EBlockCodeExtension
    {
        public static bool IsObject(this EBlockCode code)
        {
            var raw = (int)code;
            return raw is > 0 and < 100;
        }
        
        public static bool IsConnectors(this EBlockCode code)
        {
            var raw = (int)code;
            return raw is > 100 and < 200;
        }
        
        public static bool IsPurpose(this EBlockCode code)
        {
            var raw = (int)code;
            return raw is >= 200 and < 300;
        }
        
        public static bool IsSubject(this EBlockCode code)
        {
            var raw = (int)code;
            return raw is > 300 and < 400;
        }

        public static EBlockCode ToObject(this EBlockCode subject)
        {
            if (!subject.IsSubject())
            {
                throw new ArgumentException("Subject가 아닙니다.");
            }
            
            var raw = (int)subject;
            return (EBlockCode)(raw - 300);
        }
        
        public static EBlockCode ToSubject(this EBlockCode obj)
        {
            if (!obj.IsObject())
            {
                throw new ArgumentException("Object가 아닙니다.");
            }
            
            var raw = (int)obj;
            return (EBlockCode)(raw + 300);
        }
        
        public static Vector2Int ToVector2Int(this EMoveDirection direction)
        {
            return direction switch
            {
                EMoveDirection.Up => Vector2Int.up,
                EMoveDirection.Down => Vector2Int.down,
                EMoveDirection.Left => Vector2Int.left,
                EMoveDirection.Right => Vector2Int.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}