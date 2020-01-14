using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputs m_playerInputs;

        private Transform m_transform;
        private Rigidbody2D m_rigidbody;

        private Vector2 m_initialPosition;
        private Vector2 m_direction = Vector2.zero;

        [Header("MOVEMENTS PARAMETERS")]
        [SerializeField] private float m_speed;

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

        void FixedUpdate()
        {

        }

        private void GetComponents()
        {
            m_playerInputs = GetComponent<PlayerInputs>();
            m_transform = GetComponent<Transform>();
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void InitComponents()
        {
            if (m_rigidbody != null)
            {
                m_rigidbody.gravityScale = 0.0f;
                m_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (m_transform != null)
            {
                m_initialPosition = transform.localPosition;
            }
        }
    }
}