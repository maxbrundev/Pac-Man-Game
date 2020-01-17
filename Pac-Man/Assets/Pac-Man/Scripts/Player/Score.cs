using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class Score : MonoBehaviour
    {
        public delegate void ScoreDelegate(uint p_value);
        public event ScoreDelegate ScoreChangedEvent;
        public event ScoreDelegate BestScoreChangedEvent;

        private uint m_actualScore = 0;
        private uint m_bestScore = 0;


        public void AddScorePoints(uint p_value)
        {
            m_actualScore += p_value;

            if (ScoreChangedEvent != null)
                ScoreChangedEvent(m_actualScore);

            if (m_actualScore > m_bestScore)
            {
                m_bestScore = m_actualScore;

                if (BestScoreChangedEvent != null)
                    BestScoreChangedEvent(m_bestScore);
            }
        }

        public void Setup()
        {
            m_actualScore = 0;

            if (ScoreChangedEvent != null)
                ScoreChangedEvent(m_actualScore);
        }
    }
}
