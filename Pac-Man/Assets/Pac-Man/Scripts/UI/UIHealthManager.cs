using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PacMan
{
    public class UIHealthManager : MonoBehaviour
    {
        public Image m_healthBar;

        private float m_fillAmount;
        private float m_targetFillAmount;

        // Start is called before the first frame update
        void Start()
        {
            ListenEvents();
            InitImageFill();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateHealthUI();
        }

        private void ListenEvents()
        {
            FindObjectOfType<PlayerHealth>().HealthChangedEvent += OnHealthChanged;
        }

        private void InitImageFill()
        {
            m_fillAmount = m_healthBar.fillAmount;
            m_targetFillAmount = m_fillAmount;
        }

        private void OnHealthChanged(float p_value)
        {
            m_targetFillAmount = p_value;
        }

        private void UpdateHealthUI()
        {
            m_fillAmount = Mathf.Lerp(m_fillAmount, m_targetFillAmount, 5.0f * Time.deltaTime);
            m_healthBar.fillAmount = m_fillAmount;
        }
    }
}
