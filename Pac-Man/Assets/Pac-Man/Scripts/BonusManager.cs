using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class BonusManager : MonoBehaviour
    {
        public List<PacMan.BonusPickup> m_bonusList = new List<PacMan.BonusPickup>();
        private bool m_isRespawnBonusNeeded = false;

        // Start is called before the first frame update
        void Start()
        {
            foreach (PacMan.BonusPickup bonus in m_bonusList)
            {
                bonus.EnablePickup();
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckBonusList();
        }

        private void CheckBonusList()
        {
            foreach (PacMan.BonusPickup bonus in m_bonusList)
            {
                if (bonus.m_isDisable == true)
                {
                    m_isRespawnBonusNeeded = true;
                }
                else
                {
                    m_isRespawnBonusNeeded = false;
                }
            }
        }
    }
}
