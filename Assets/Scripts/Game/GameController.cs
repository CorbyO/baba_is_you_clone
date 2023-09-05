using Puzzle.Utils;
using UnityEngine;

namespace Puzzle.Game
{
    public class GameController
    {
        public void Initialize()
        {
        }

        public void NextTurn()
        {
            if (GameManager.Instance.MapController.IsWin())
            {
                Debug.Log("Win");
                App.Quit();
            }
        }
    }
}