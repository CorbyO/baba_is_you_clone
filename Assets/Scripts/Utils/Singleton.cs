namespace Puzzle.Utils
{
    public abstract class Singleton<T>
        where T : Singleton<T>, new()
    {
        public static T Instance { get; private set; }

        protected abstract void OnInitialize();

        public void Initialize()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = new();

            OnInitialize();
        }
        
        public void Initialize(T instance)
        {
            if (Instance != null)
            {
                return;
            }

            Instance = instance;

            OnInitialize();
        }
    }
}