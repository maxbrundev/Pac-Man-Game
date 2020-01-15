using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class Ghost : MonoBehaviour
    {
        [Header("ENEMY PARAMETERS")]
        [SerializeField] private float m_speed;

        public Transform[] m_wayPoints;

        private float m_wayPointDistance;
        private int m_randomPoint;

        // Start is called before the first frame update
        void Start()
        {
            m_randomPoint = Random.Range(0, m_wayPoints.Length);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, m_wayPoints[m_randomPoint].transform.position, m_speed * Time.deltaTime);
            m_wayPointDistance = Vector2.Distance(transform.position, m_wayPoints[m_randomPoint].position);

            if (m_wayPointDistance < 0.1f)
            {
                m_randomPoint = Random.Range(0, m_wayPoints.Length);
            }
        }
    }
}
