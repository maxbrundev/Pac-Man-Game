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
        private Health m_health;
        private Score m_score;

        // Start is called before the first frame update
        void Start()
        {
            GetComponents();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void GetComponents()
        {
            m_controller = GetComponent<PlayerController>();
            m_health = GetComponent<Health>();
            m_score = GetComponent<Score>();

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

            Setup();
        }
    }
}
