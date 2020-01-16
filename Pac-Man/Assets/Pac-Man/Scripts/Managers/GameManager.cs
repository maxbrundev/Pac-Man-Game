using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class GameManager : MonoBehaviour
    {
        private CoinsManager m_coinsManager;

        private PlayerController m_playerController;
        private Score m_playerScore;

        public GameObject m_playerPrefab;
        public Transform m_spawnPoint;

        // Start is called before the first frame update
        void Start()
        {
            SpawnPlayer();
            FindObjects();
            LinstenEvents();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FindObjects()
        {
            m_coinsManager      = FindObjectOfType<CoinsManager>();
            m_playerController  = FindObjectOfType<PlayerController>();
            m_playerScore       = FindObjectOfType<Score>();
        }

        private void LinstenEvents()
        {
            FindObjectOfType<Health>().DeathEvent += OnGameOver;
        }

        private void OnGameOver()
        {
            ResetPlayerScore();
            ResetCoins();
            RespawnPlayer();
        }

        private void SpawnPlayer()
        {
            m_playerPrefab = Instantiate(m_playerPrefab, m_spawnPoint.position, m_spawnPoint.rotation) as GameObject;
        }

        private void RespawnPlayer()
        {
            m_playerController.StopMovement();
            m_playerController.SetPosition(m_spawnPoint.position);
        }

        private void ResetCoins()
        {
            m_coinsManager.RespawnCoins();
        }

        private void ResetPlayerScore()
        {
            m_playerScore.ResetScore();
        }
    }
}
