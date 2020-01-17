using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    /**
	* A class that encapsulate the colliding and detection logic
	*/
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerDetector : MonoBehaviour
    {
        //public delegate void CollisionDelegate();
        //public event CollisionDelegate WallEvent;
        //
        //public delegate void EatEnemyDelegate();
        //public event EatEnemyDelegate EatEnemyEvent;

        private BoxCollider2D m_collider;
        private Vector2 m_colliderEnd;

        public float m_value = 1.0f;
        public float m_radius = 1.0f;

        void Awake()
        {
            GetComponents();
        }

        // Start is called before the first frame update
        void Start()
        {
            InitComponents();
        }

        // Update is called once per frame
        void Update()
        {
            Collider2D hitedCollider = Physics2D.OverlapCircle(m_colliderEnd, m_radius);
        }

        private void GetComponents()
        {
            m_collider = GetComponent<BoxCollider2D>();
        }

        private void InitComponents()
        {
            if(m_collider != null)
            {
                m_colliderEnd = (Vector2)transform.position + (Vector2)transform.right * m_value;
            }
        }
    }
}
