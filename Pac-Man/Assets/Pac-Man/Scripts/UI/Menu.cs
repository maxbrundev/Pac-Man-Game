using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    public class Menu : MonoBehaviour
    {
        private GameManager m_gameManager;

        private void Start()
        {
            m_gameManager = GameManager.Instance;
            m_gameManager.SetGameState(GameManager.GameState.MENU);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            m_gameManager.SetGameState(GameManager.GameState.GAME);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
