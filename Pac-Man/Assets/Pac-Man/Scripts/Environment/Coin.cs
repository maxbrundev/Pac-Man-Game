using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class Coin : MonoBehaviour
    {
        private SpriteRenderer m_sprite;

        private bool m_enable = false;

        [Header("BONUS PARAMETERS")]
        [SerializeField] private uint m_value;

        public bool Enabled
        {
            get
            {
                return m_enable;
            }
        }

        void Awake()
        {
            GetComponents();
        }

        void Start()
        {
            EnableSpriteRenderer(true);
        }

        private void GetComponents()
        {
            m_sprite = GetComponentInChildren<SpriteRenderer>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                if (!m_enable)
                {
                    Score score = col.gameObject.GetComponent<Score>();

                    if (score != null)
                        score.AddScorePoints(m_value);

                    DisableCoin();
                }
            }
        }

        public void DisableCoin()
        {
            m_enable = true;
            EnableSpriteRenderer(false);
        }

        public void EnableCoin()
        {
            m_enable = false;
            EnableSpriteRenderer(true);
        }

        private void EnableSpriteRenderer(bool p_value)
        {
            m_sprite.enabled = p_value;
        }
    }
}
