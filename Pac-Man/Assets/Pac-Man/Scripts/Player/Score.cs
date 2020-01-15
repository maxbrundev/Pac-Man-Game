using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class Score : MonoBehaviour
    {
        public delegate void ScoreDelegate(uint p_value);
        public event ScoreDelegate ScoreChangedEvent;

        private uint m_actualScore = 0;
        private uint m_bestScore = 0;

        public void AddScorePoints(uint p_value)
        {
            m_actualScore += p_value;

            ScoreChangedEvent(m_actualScore);
        }
    }
}
