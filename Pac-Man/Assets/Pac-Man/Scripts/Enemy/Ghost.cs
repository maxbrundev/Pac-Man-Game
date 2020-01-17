using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    /**
	* A class that encapsulate the Ghost behaviour, the movements behaviour is temporary
    * (I need to create a 2D Grid class that represent the level and store the center position of each nodes
    * With this grid i can move the Ghosts and the player case by case and cancel the movements if a case is already owned by a wall sprite)
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
            //temporary movements
            //Move();
            CollisionCheck();
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
            m_pointIndex = Random.Range(m_pointIndex - 1, m_wayPoints.Count);
            m_pointIndex = Mathf.Clamp(m_pointIndex, 0, m_wayPoints.Count);
        }

        private void Move()
        {
            Vector3 targetPosition = m_wayPoints[m_pointIndex].localPosition;
            float distanceFromTarget = Vector2.Distance(targetPosition, transform.position);

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, m_speed * Time.deltaTime);
            
            if (distanceFromTarget < 0.05f)
            {
                CheckWayPoints();
            }
        }

        private void CollisionCheck()
        {
            uint collisions = 0;

            Vector2 colliderEnd = transform.position + Vector3.up * 0.5f;
            Collider2D[] hitedCollider = Physics2D.OverlapCircleAll(colliderEnd, 0.20f);

            foreach (Collider2D hitCollider in hitedCollider)
            {
                if (hitCollider.tag != gameObject.tag)
                {
                    if(hitCollider.tag == "Player")
                    {
                        TakeDamage(1);
                        if (KilledEvent != null)
                            KilledEvent(m_coinsToWin);
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!m_isDead)
            {
                if (col.gameObject.tag == "Player")
                {
                    ApplyDamage(1);
                }
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, 0.20f);
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
