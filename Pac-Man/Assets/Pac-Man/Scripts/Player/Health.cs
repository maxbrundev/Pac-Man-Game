using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class Health : MonoBehaviour
    {
        public delegate void HealthDelegate(float p_value);
        public event HealthDelegate HealthChangedEvent;

        public delegate void DeathDelegate();
        public event DeathDelegate DeathEvent;

        private SpriteRenderer m_playerSprite;

        [Header("HEALTH PARAMETERS")]
        [SerializeField] private uint m_maxHealth = 100;
        [SerializeField] private float m_dammageAnimationSpeed = 2.0f;

        private uint m_currentHealth;
        private bool isDead = false;

        private Color m_colorDefault;
        private Color m_colorTarget;

        void Awake()
        {
            GetComponents();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_colorDefault = m_playerSprite.color;
            m_currentHealth = m_maxHealth;
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

        public void ApplyDammage(uint p_amount)
        {
            if (p_amount > m_currentHealth)
                m_currentHealth = 0;
            else
                m_currentHealth -= p_amount;

            if (m_currentHealth == 0)
                isDead = true;
                
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

            HealthChangedEvent(GetHealthPercentage());
        }

        private void UpdateColorAnimation()
        {
            if (m_currentHealth < 2)
                SetDammageColor();
            else if (m_currentHealth > 2)
                ResetDefaultColor();

            Color currentColor = m_playerSprite.color;
            Color colorToReach = m_colorTarget;
            m_playerSprite.color = new Color(Mathf.Lerp(currentColor.r, colorToReach.r, Time.deltaTime * m_dammageAnimationSpeed), Mathf.Lerp(currentColor.g, colorToReach.g, Time.deltaTime * m_dammageAnimationSpeed), Mathf.Lerp(currentColor.b, colorToReach.b, Time.deltaTime * m_dammageAnimationSpeed), currentColor.a);
        }

        public void SetDammageColor()
        {
            m_colorTarget = Color.red;
        }

        public void ResetDefaultColor()
        {
            m_colorTarget = m_colorDefault;
        }

        private void CheckDeath()
        {
            if(isDead)
            {
                if (DeathEvent != null)
                {
                    DeathEvent();
                   
                }

                Setup();
            }
        }

        public void Setup()
        {
            isDead = false;
            m_currentHealth = m_maxHealth;

            HealthChangedEvent(GetHealthPercentage());
        }

        private float GetHealthPercentage()
        {
            return (float)m_currentHealth / (float)m_maxHealth;
        }
    }
}