using Puzzle.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Puzzle.Lobby
{
    public class GameManager : MonoBehaviour
    {
        public void GameStart()
        {
            Debug.Log("Game Start");

            SceneManager.LoadSceneAsync("Scenes/GameScene");
        }

        public void GameEnd()
        {
            Debug.Log("Game End.");
            
            App.Quit();
        }
    }
}