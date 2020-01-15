using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    /**
	* A class that encapsulates the Player movements logic
	*/
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputs m_playerInputs;

        private Transform m_transform;
        private Rigidbody2D m_rigidbody;
        private SpriteRenderer m_playerSprite;

        private Vector2 m_initialPosition;
        private Vector2 m_velocityDirection = Vector2.zero;
        private Vector2 m_velocityRef = Vector2.zero;

        private float horizontalInput;
        private float verticalInput;
        private int m_rotationCoef;

        private float leftScreenLimit;
        private float rightScreenLimit;
        private float upScreenLimit;
        private float downScreenLimit;

        private bool m_isFacingRight = true;

        [Header("MOVEMENTS PARAMETERS")]
        [SerializeField] private float m_speed;

        void Awake()
        {
            GetComponents();
            InitScreenLimits();
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
            CheckHorizontalOrientation();
            CheckOutScreen();
        }

        void FixedUpdate()
        {
            Move();
        }

        private void GetComponents()
        {
            m_playerInputs = GetComponent<PlayerInputs>();
            m_transform = GetComponent<Transform>();
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_playerSprite = GetComponentInChildren<SpriteRenderer>();
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

        private void InitScreenLimits()
        {
            leftScreenLimit = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
            rightScreenLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x;

            upScreenLimit = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f)).y;
            downScreenLimit = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        }

        private void CheckOutScreen()
        {
            Vector3 newPosition = transform.position;

            if (transform.position.x < leftScreenLimit)
            {
                newPosition.x = rightScreenLimit;
            }
            else if (transform.position.x > rightScreenLimit)
            {
                newPosition.x = leftScreenLimit;
            }

            if(transform.position.y < downScreenLimit)
            {
                newPosition.y = upScreenLimit;
            }
            else if (transform.position.y > upScreenLimit)
            {
                newPosition.y = downScreenLimit;
            }

            transform.position = newPosition;
        }

        private void CalculateVelocityDirection()
        {
            horizontalInput = m_playerInputs.AxisInputRaw.x;
            verticalInput   = m_playerInputs.AxisInputRaw.y;

            //calculate the coefficient of Z Axis rotation depending of the player vertical input direction
            m_rotationCoef = verticalInput > 0.5? 1 : -1;

            if (horizontalInput > 0.5f || horizontalInput < -0.5f)
            {
                m_velocityDirection = Vector2.right * horizontalInput;

                RotateSpriteTransform(0.0f);
            }
            else if (verticalInput > 0.5f || verticalInput < -0.5f)
            {
                m_velocityDirection = Vector2.up * verticalInput;

                RotateSpriteTransform(90.0f);
            }

            m_velocityDirection.Normalize();

            m_velocityDirection *= m_speed;
        }

        private void RotateSpriteTransform(float p_angleTargetValue)
        {
            //if the player sprite transform is already flipped then I flip it back for a correct facing orientation
            if (m_playerSprite.transform.localScale.x < 0.0f)
                FlipScale();

            m_playerSprite.transform.localRotation = Quaternion.AngleAxis(p_angleTargetValue * m_rotationCoef, Vector3.forward);
        }

        private void CheckHorizontalOrientation()
        {
            if (horizontalInput > 0.0f && !m_isFacingRight)
            {
                FlipScale();
            }
            else if (horizontalInput < 0.0f && m_isFacingRight)
            {
                FlipScale();
            }
        }

        private void Move()
        {
            m_rigidbody.velocity = m_velocityDirection * Time.fixedDeltaTime;
        }

        private void FlipScale()
        {
            m_isFacingRight = !m_isFacingRight;

            Vector3 flippedScale = m_playerSprite.transform.localScale;
            flippedScale.x *= -1;

            m_playerSprite.transform.localScale = flippedScale;
        }
    }
}