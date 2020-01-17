using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class PlayerManager : MonoBehaviour
    {
        public delegate void PlayerDieDelegate();
        public event PlayerDieDelegate PlayerDieEvent;

        public Transform m_spawnPoint;

        private PlayerController m_controller;
        private PlayerHealth m_health;
        private PlayerScore m_score;

        void Awake()
        {
            GetComponents();
        }

        // Start is called before the first frame update
        void Start()
        {
            ListenEvents();
            FindObjects();
        }

        private void GetComponents()
        {
            m_controller = GetComponent<PlayerController>();
            m_health = GetComponent<PlayerHealth>();
            m_score = GetComponent<PlayerScore>();
        }

        private void ListenEvents()
        {
            m_health.DeathEvent += OnDead;
        }

        private void FindObjects()
        {
            var ghosts = FindObjectsOfType<Ghost>();

            foreach (var ghost in ghosts)
            {
                ghost.ApplyDamageEvent += OnTakeDamage;
                ghost.KilledEvent += OnWinCoins;
            }
        }

        private void OnWinCoins(uint p_amount)
        {
            m_score.AddScorePoints(p_amount);
        }

        private void OnTakeDamage(uint p_amount)
        {
            m_health.TakeDamage(p_amount);
        }

        private void LinstenEvents()
        {
            if (m_health != null)
                m_health.DeathEvent += OnDead;
        }

        public void Setup()
        {
            m_controller.SetNewPosition(m_spawnPoint.position);
            m_health.Setup();
            m_score.Setup();
        }

        private void OnDead()
        {
            if(PlayerDieEvent != null)
                PlayerDieEvent();
        }
    }
}
