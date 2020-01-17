using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PacMan
{
    /**
	* this class contain references on other managers and define the game context depending of his actual state (Restart the Game, clean the scene...) 
	*/
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            MENU,
            GAME,
            PAUSE
        }

        private static GameManager instance = null;

        private PlayerManager m_playerManager;
        private CoinsManager m_coinsManager;

        public delegate void GameStateDelegate();
        public event GameStateDelegate GameStateChangedEvent;

        public GameState m_state;

        public static GameManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameManager();
                    DontDestroyOnLoad(instance);
                }

                return instance;
            }
        }

        //private Constructor for preventing creation of instances
        private GameManager()
        {

        }

        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            FindObjects();
            LinstenEvents();
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(instance);
        }

        public void SetGameState(GameState p_state)
        {
            m_state = p_state;

            if(GameStateChangedEvent != null)
                GameStateChangedEvent();
        }

        private void OnApplicationQuit()
        {
            instance = null;
        }

        private void FindObjects()
        {
            m_playerManager = FindObjectOfType<PlayerManager>();
            m_coinsManager  = FindObjectOfType<CoinsManager>();
        }

        private void LinstenEvents()
        {
            if(m_playerManager)
                m_playerManager.PlayerDieEvent += OnGameOver;
        }

        private void OnGameOver()
        {
            RestartGame();
        }

        private void ResetCoins()
        {
            m_coinsManager.ResetCoins();
        }

        public void RestartGame()
        {
            m_playerManager.Setup();
            ResetCoins();
        }
    }
}
