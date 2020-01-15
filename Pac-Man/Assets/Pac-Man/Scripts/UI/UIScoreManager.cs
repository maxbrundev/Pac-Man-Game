using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PacMan
{
    public class UIScoreManager : MonoBehaviour
    {
        public Text m_scoreText;
        private uint m_scoreAmount = 0;

        // Start is called before the first frame update
        void Start()
        {
            FindObjectOfType<Score>().ScoreChangedEvent += OnScore;
        }

        // Update is called once per frame
        void Update()
        {
            m_scoreText.text = "Score:" + m_scoreAmount;
        }

        private void OnScore(uint p_value)
        {
            m_scoreAmount = p_value;
        }
    }
}

