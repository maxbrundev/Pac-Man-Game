using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class BonusPickup : MonoBehaviour
    {
        private SpriteRenderer m_sprite;

        [HideInInspector] public bool m_isDisable = false;

        [Header("BONUS PARAMETERS")]
        [SerializeField] private uint m_value;

        void Awake()
        {
            GetComponents();
        }

        void Start()
        {
            EnablePickup();
        }

        private void GetComponents()
        {
            m_sprite = GetComponentInChildren<SpriteRenderer>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                if (!m_isDisable)
                {
                    Score score = col.gameObject.GetComponent<Score>();

                    if (score != null)
                        score.AddScorePoints(m_value);

                    //simple disable, cheaper than destroy the object
                    DisablePickup();
                }
            }
        }

        public void DisablePickup()
        {
            m_isDisable = true;
            m_sprite.transform.gameObject.SetActive(false);
        }

        public void EnablePickup()
        {
            m_isDisable = false;
            m_sprite.transform.gameObject.SetActive(true);
        }
    }
}
