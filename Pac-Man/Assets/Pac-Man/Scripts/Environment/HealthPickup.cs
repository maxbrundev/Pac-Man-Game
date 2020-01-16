using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    /**
	* A class that contain the health bonus behaviour
	*/
    public class HealthPickup : MonoBehaviour
    {
        [Header("HEAL PARAMETERS")]
        [SerializeField] private uint m_value;

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                Health healthBehaviour = col.gameObject.GetComponent<Health>();

                if (healthBehaviour != null)
                    healthBehaviour.Heal(m_value);
            }
        }
    }
}
