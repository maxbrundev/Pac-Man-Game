﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class Coin : MonoBehaviour
    {
        public delegate void CoinDelegate();
        public event CoinDelegate CoinEvent;

        private SpriteRenderer m_sprite;

        private bool m_enableCoin = true;

        [Header("BONUS PARAMETERS")]
        [SerializeField] private uint m_value;

        public bool EnabledCoin
        {
            get
            {
                return m_enableCoin;
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
                if (m_enableCoin)
                {
                    Score score = col.gameObject.GetComponent<Score>();
                    if (score != null)
                        score.AddScorePoints(m_value);

                    DisableCoin();

                    if (CoinEvent != null)
                        CoinEvent();
                }
            }
        }

        public void DisableCoin()
        {
            m_enableCoin = false;
            EnableSpriteRenderer(false);
        }

        public void EnableCoin()
        {
            m_enableCoin = true;
            EnableSpriteRenderer(true);
        }

        private void EnableSpriteRenderer(bool p_value)
        {
            m_sprite.enabled = p_value;
        }
    }
}