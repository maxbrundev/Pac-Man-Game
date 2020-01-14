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
            CalculateVelocityDirection();

            m_velocityDirection *= m_speed;

            Move(m_velocityDirection);

            //m_rigidbody.velocity = direction * Time.deltaTime;
        }

        void FixedUpdate()
        {

        }

        private void CalculateVelocityDirection()
        {
            float horizontalInput = m_playerInputs.inputRaw.x;
            float verticalInput = m_playerInputs.inputRaw.y;

            if (horizontalInput > 0.5f || horizontalInput < -0.5f)
            {
                m_velocityDirection = Vector2.right * horizontalInput;
            }
            else if (verticalInput > 0.5f || verticalInput < -0.5f)
            {
                m_velocityDirection = Vector2.up * verticalInput;
            }

            m_velocityDirection.Normalize();
        }

        private void Move(Vector2 p_targetDirection)
        {
            Vector2 smoothedVelocity = Vector2.SmoothDamp(m_rigidbody.velocity, p_targetDirection, ref m_velocityRef, m_smoothTime);

            m_rigidbody.velocity = smoothedVelocity;
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