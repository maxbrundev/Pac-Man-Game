using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    /**
	* A class that contain all the Coins in a container and handle an event that each coin send when they are eated, this event call a verification of the actual state of the all coins and determine a global respawn or not
	*/
    public class CoinsManager : MonoBehaviour
    {
        private List<Coin> m_totalCoins = new List<Coin>();

        private bool m_needRespawn = false;

        // Start is called before the first frame update
        void Start()
        {
            InitCoinList();
        }

        private void InitCoinList()
        {
            Coin[] Coins = FindObjectsOfType<Coin>();

            for (int i = 0; i < Coins.Length; i++)
            {
                Coins[i].CoinEvent += OnCoin;
                m_totalCoins.Add(Coins[i]);
            }
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

        public void RespawnCoins()
        {
            foreach(Coin coin in m_totalCoins)
            {
                coin.EnableCoin();
            }
        }
    }
}
