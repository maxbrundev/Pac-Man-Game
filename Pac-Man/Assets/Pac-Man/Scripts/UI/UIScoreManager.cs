using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PacMan
{
    public class UIScoreManager : MonoBehaviour
    {
        public Text m_scoreText;
        public Text m_bestScoreText;

        private uint m_scoreAmount = 0;
        private uint m_bestScoreAmount = 0;

        // Start is called before the first frame update
        void Start()
        {
            FindObjectOfType<Score>().ScoreChangedEvent += OnScore;
            FindObjectOfType<Score>().BestScoreChangedEvent += OnBestScore;
        }

        // Update is called once per frame
        void Update()
        {
            m_scoreText.text = "Score:" + m_scoreAmount;
            m_bestScoreText.text = "Best Score:" + m_bestScoreAmount;
        }

        private void OnScore(uint p_value)
        {
            m_scoreAmount = p_value;
        }

        private void OnBestScore(uint p_value)
        {
            m_bestScoreAmount = p_value;
        }
    }
}

