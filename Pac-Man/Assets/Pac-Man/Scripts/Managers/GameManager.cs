using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            MENU,
            GAME
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

        private GameManager()
        {

        }

        private void Update()
        {
            
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

            if (m_state == GameState.GAME)
            {
                FindObjects();
                LinstenEvents();

                if (m_playerManager)
                    m_playerManager.Setup();
            }
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
            ResetCoins();
        }

        private void ResetCoins()
        {
            m_coinsManager.RespawnCoins();
        }
    }
}
