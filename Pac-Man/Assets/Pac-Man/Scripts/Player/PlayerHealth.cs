using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class PlayerHealth : MonoBehaviour
    {
        public delegate void HealthDelegate(float p_value);
        public event HealthDelegate HealthChangedEvent;

        public delegate void DeathDelegate();
        public event DeathDelegate DeathEvent;

        private SpriteRenderer m_playerSprite;

        private Color m_colorDefault;
        private Color m_colorTarget;

        private uint m_currentHealth;

        private float m_damageAnimationSpeed = 2.0f;

        private bool m_isDead = false;

        [Header("HEALTH PARAMETERS")]
        [SerializeField] private uint m_maxHealth = 100;

        void Awake()
        {
            GetComponents();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_colorDefault = m_playerSprite.color;
            Setup();
        }

        void Update()
        {
            UpdateColorAnimation();
            CheckDeath();
        }

        private void GetComponents()
        {
            m_playerSprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void Setup()
        {
            m_isDead = false;
            m_currentHealth = m_maxHealth;

            if(HealthChangedEvent != null)
                HealthChangedEvent(GetHealthPercentage());
        }

        public void TakeDamage(uint p_amount)
        {
            if (p_amount > m_currentHealth)
                m_currentHealth = 0;
            else
                m_currentHealth -= p_amount;

            if (m_currentHealth == 0)
                m_isDead = true;

            if (HealthChangedEvent != null)
                HealthChangedEvent(GetHealthPercentage());
        }

        public void Heal(uint p_amount)
        {
            if (p_amount > m_maxHealth)
                m_currentHealth = m_maxHealth;
            else
                m_currentHealth += p_amount;

            if (m_currentHealth > m_maxHealth)
                m_currentHealth = m_maxHealth;

            if (HealthChangedEvent != null)
                HealthChangedEvent(GetHealthPercentage());
        }

        private void UpdateColorAnimation()
        {
            if (m_currentHealth < 2)
                SetDamageColor();
            else if (m_currentHealth > 2)
                ResetDefaultColor();

            Color currentColor = m_playerSprite.color;
            Color colorToReach = m_colorTarget;
            m_playerSprite.color = new Color(Mathf.Lerp(currentColor.r, colorToReach.r, Time.deltaTime * m_damageAnimationSpeed), Mathf.Lerp(currentColor.g, colorToReach.g, Time.deltaTime * m_damageAnimationSpeed), Mathf.Lerp(currentColor.b, colorToReach.b, Time.deltaTime * m_damageAnimationSpeed), currentColor.a);
        }

        public void SetDamageColor()
        {
            m_colorTarget = Color.red;
        }

        public void ResetDefaultColor()
        {
            m_colorTarget = m_colorDefault;
        }

        private void CheckDeath()
        {
            if(m_isDead)
            {
                if (DeathEvent != null)
                    DeathEvent();
            }
        }

        private float GetHealthPercentage()
        {
            return (float)m_currentHealth / (float)m_maxHealth;
        }
    }
}