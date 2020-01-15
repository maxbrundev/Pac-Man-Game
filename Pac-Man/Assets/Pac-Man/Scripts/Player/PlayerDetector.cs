using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    /**
	* A class that encapsulates the colliding and detection logic
	*/
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerDetector : MonoBehaviour
    {
        public delegate void CollisionDelegate();
        public event CollisionDelegate WallEvent;

        private BoxCollider2D m_collider;

        public float m_value;

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

        }

        private void GetComponents()
        {
            m_collider = GetComponent<BoxCollider2D>();
        }

        private void InitComponents()
        {
            if(m_collider != null)
            {
            }
        }

        //Trying to define a wall detection circle
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.localPosition + transform.forward * m_value, 0.15f);
        }
    }
}
