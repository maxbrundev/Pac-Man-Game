using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    /**
	* A class that represent the Ghost behaviour
	*/
    public class Ghost : MonoBehaviour, IEnemyActor
    {
        public delegate void GhostDelegate(uint p_amount);
        public event GhostDelegate ApplyDamageEvent;
        public event GhostDelegate KilledEvent;

        public List<Transform> m_wayPoints = new List<Transform>();

        private BoxCollider2D m_collider;
        private SpriteRenderer m_spriteRenderer;

        private Vector2 m_spawnPosition;

        private bool m_isDead = false;

        private uint m_healthPoints;

        [Header("ENEMY PARAMETERS")]
        [SerializeField] private uint m_maxHealthPoints;

        [SerializeField] private uint m_damagePoints;
        [SerializeField] private uint m_coinsToWin;

        [SerializeField] private float m_speed;
        [SerializeField] private float m_moveToSpawnSpeed;

        private int m_pointIndex = 0;

        void Awake()
        {
            GetComponents();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_spawnPosition = (Vector2)transform.position;

            Setup();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            CheckisDead();
        }

        private void GetComponents()
        {
            m_collider = GetComponent<BoxCollider2D>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Setup()
        {
            m_isDead = false;
            m_healthPoints = m_maxHealthPoints;

            EnableCollisions();
        }

        private void CheckisDead()
        {
            if (m_isDead)
            {
                DisableCollisions();
                Spawn();
            }
            
        }

        private void CheckWayPoints()
        {
            for (int i = 0; i < m_wayPoints.Count; i++)
            {
                float distance = Vector3.Distance(m_wayPoints[i].position, transform.position);
            }

            m_pointIndex++;

            if (m_pointIndex >= m_wayPoints.Count)
            {
                m_pointIndex = 0;
            }
        }

        private void Move()
        {
            Vector3 targetPosition = m_wayPoints[m_pointIndex].position;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 2.0f * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                CheckWayPoints();
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if(!m_isDead)
            {
                if (col.gameObject.tag == "Player")
                {
                    Vector2 playerPos = (Vector2)col.gameObject.transform.position;
                    float angle = Vector2.SignedAngle((Vector2)transform.up, playerPos - (Vector2)transform.position);

                    if (angle > 0.0f)
                    {
                        Debug.Log("AIE");
                        TakeDamage(1);

                        if (KilledEvent != null)
                            KilledEvent(m_coinsToWin);
                    }
                    else
                    {
                        Debug.Log("BOOM");
                        ApplyDamage(1);
                    }
                }
            }
        }

        private void DisableCollisions()
        {
            m_collider.enabled = false;
        }

        private void EnableCollisions()
        {
            m_collider.enabled = true;
        }

        public void Move(Vector2 p_direction)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_wayPoints[m_pointIndex].transform.position, m_speed * Time.deltaTime);
        }

        public void TakeDamage(uint p_amount)
        {
            if (p_amount > m_healthPoints)
                m_healthPoints = 0;
            else
                m_healthPoints -= p_amount;

            if (m_healthPoints == 0)
            {
                m_isDead = true;
            }
        }

        public void ApplyDamage(uint p_amount)
        {
            if (ApplyDamageEvent != null)
                ApplyDamageEvent(p_amount);
        }

        public void Spawn()
        {
            transform.position = m_spawnPosition;
            Setup();
        }
    }
}
