using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class CameraResize : MonoBehaviour
    {
        public SpriteRenderer m_backgroundSprite;

        // Use this for initialization
        void Start()
        {
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = m_backgroundSprite.bounds.size.x / m_backgroundSprite.bounds.size.y;

            float differenceInSize = targetRatio / screenRatio;

            Camera.main.orthographicSize = screenRatio >= targetRatio ? m_backgroundSprite.bounds.size.y / 2 : m_backgroundSprite.bounds.size.y / 2 * differenceInSize;
        }
    }
}