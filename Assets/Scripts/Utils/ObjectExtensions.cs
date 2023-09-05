namespace Puzzle.Utils
{
    public static class ObjectExtensions
    {
        public static void TryThrowNull(this object obj)
        {
            if (obj == null)
            {
                throw new System.NullReferenceException();
            }
        }
    }
}