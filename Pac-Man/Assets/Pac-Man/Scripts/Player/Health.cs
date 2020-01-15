using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class Health : MonoBehaviour
    {
        public delegate void HealthDelegate(float p_value);
        public event HealthDelegate HealthChangedEvent;

        [Header("HEALTH PARAMETERS")]
        [SerializeField] private uint m_maxHealth = 100;

        private uint m_currentHealth;
        private float m_healthPercentage;
        private bool isDead = false;

        // Start is called before the first frame update
        void Start()
        {
            m_currentHealth = m_maxHealth;
        }

        public void ApplyDammage(uint p_amount)
        {
            if (p_amount > m_currentHealth)
                m_currentHealth = 0;
            else
                m_currentHealth -= p_amount;

            if (m_currentHealth == 0)
                isDead = true;
                
            m_healthPercentage = (float)m_currentHealth / (float)m_maxHealth;
            HealthChangedEvent(m_healthPercentage);
        }

        public void Heal(uint p_amount)
        {
            if (p_amount > m_maxHealth)
                m_currentHealth = m_maxHealth;
            else
                m_currentHealth += p_amount;

            if (m_currentHealth > m_maxHealth)
                m_currentHealth = m_maxHealth;

            m_healthPercentage = (float)m_currentHealth / (float)m_maxHealth;
            HealthChangedEvent(m_healthPercentage);
        }
    }
}