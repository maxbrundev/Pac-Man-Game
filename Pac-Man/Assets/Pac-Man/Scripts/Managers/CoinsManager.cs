using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class CoinsManager : MonoBehaviour
    {
        public List<Coin> m_totalCoins = new List<Coin>();

        private bool m_needRespawn = false;

        // Start is called before the first frame update
        void Start()
        {
            foreach(Coin coin in m_totalCoins)
            {
                coin.CoinEvent += OnCoin;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCoin()
        {
            m_needRespawn = true;

            for (int i = 0; i < m_totalCoins.Count - 1; i++)
            {
                for (int j = 0; j < m_totalCoins.Count - i -1; j++)
                {
                    if (m_totalCoins[j].EnabledCoin != m_totalCoins[j + 1].EnabledCoin)
                        m_needRespawn = false;
                }
            }

            if (m_needRespawn)
                RespawnCoins();
        }

        private void RespawnCoins()
        {
            foreach(Coin coin in m_totalCoins)
            {
                coin.EnableCoin();
            }
        }
    }
}
