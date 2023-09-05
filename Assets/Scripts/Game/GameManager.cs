using System;
using System.Text;
using Cysharp.Threading.Tasks;
using Puzzle.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Puzzle.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public InputController Input { get; private set; }
        public CameraController CameraController { get; private set; }
        
        public GameController GameController { get; private set; } = new();
        public PlayerController PlayerController { get; private set; } = new();
        public MapContainer MapContainer { get; private set; } = new();
        public MapReader MapReader { get; private set; } = new();
        public MapController MapController { get; private set; } = new();

        public void Awake()
        {
            Instance = this;
            Initialize();
            Load().Forget();
        }
        
        private void Initialize()
        {
            var mapRaw = new StringBuilder();
            mapRaw.AppendLine("303,100,200,  0,  0,  0,  0,  0,  0,  2")
                  .AppendLine("302,100,202,  0,  0,  0,  0,  0,  0,  0")
                  .AppendLine("  0,  0,  0,  0,  0,  0,  0,  0,  0,  0")
                  .AppendLine("  0,  0,  0,  0,  0,  2,  2,  2,  2,  0")
                  .AppendLine("  0,  0,  0,  1,  0,  4,  0,  0,  2,  0")
                  .AppendLine("  0,  0,  0,  0,  0,  2,  0,  3,  2,  0")
                  .AppendLine("  0,  0,  0,  0,  0,  2,  2,  2,  2,  0")
                      .Append("304,100,201,  0,  0,  0  ,0  ,0  ,0  ,2");

            Input = FindObjectOfType<InputController>();
            CameraController = FindObjectOfType<CameraController>();
            Input.Initialize();
            CameraController.Initialize();

            GameController.Initialize();
            PlayerController.Initialize();
            MapContainer.Initialize(mapRaw.ToString(), 1);
            MapReader.Initialize();
            MapController.Initialize();
        }

        private async UniTask Load()
        {
            await MapController.Generate(new BlockFactory());

            OnLoadComplete();
        }

        private void OnLoadComplete()
        {
            CameraController.MatchWithMap();
            MapReader.Read();

            Input.OnMovement += PlayerController.Movement;
            
            PlayerController.OnMove += MapReader.Read;
            PlayerController.OnMove += GameController.NextTurn;
        }

        public void OnDestroy()
        {
            Instance = null;
        }
    }
}