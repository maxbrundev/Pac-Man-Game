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
        private BoxCollider2D m_collider;

        [Header("COLLISION PARAMETERS")]
        [SerializeField] private float m_radius = 0.25f;

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
    }
}
