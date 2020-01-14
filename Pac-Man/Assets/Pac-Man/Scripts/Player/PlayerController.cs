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
        private Vector2 m_velocityDirection = Vector2.zero;
        private Vector2 m_velocityRef = Vector2.zero;


        private float m_smoothTime = 0.0f;

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
            float horizontalDirection = m_playerInputs.inputRaw.x;
            float verticalDirection = m_playerInputs.inputRaw.y;

            if (horizontalDirection > 0.5f || horizontalDirection < -0.5f)
            {
                m_velocityDirection = Vector2.right * horizontalDirection;
            }
            else if (verticalDirection > 0.5f || verticalDirection < -0.5f)
            {
                m_velocityDirection = Vector2.up * verticalDirection;
            }

            m_velocityDirection.Normalize();

            m_velocityDirection *= m_speed;

            Vector2 smoothedVelocity = Vector2.SmoothDamp(m_rigidbody.velocity, m_velocityDirection, ref m_velocityRef, m_smoothTime);

            m_rigidbody.velocity = smoothedVelocity;

            //m_rigidbody.velocity = direction * Time.deltaTime;
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