using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public List<PacMan.BonusPickup> m_bonusList = new List<PacMan.BonusPickup>();
    private bool m_isRespawnBonusNeeded = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(PacMan.BonusPickup bonus in m_bonusList)
        {
            bonus.BonusEatedEvent += OnCheckBonusList;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCheckBonusList()
    {
        m_isRespawnBonusNeeded = false;

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

        CheckBonusRespawn();
    }

    private void CheckBonusRespawn()
    {
        if (m_isRespawnBonusNeeded)
        {
            foreach (PacMan.BonusPickup bonus in m_bonusList)
            {
                bonus.EnablePickup();
            }

            m_isRespawnBonusNeeded = false;
        }
    }
}
