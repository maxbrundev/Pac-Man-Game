using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class ApplyDammageTest : MonoBehaviour
    {
        [Header("DAMMAGES PARAMETERS")]
        [SerializeField] private uint m_value;

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                Health healthBehaviour = col.gameObject.GetComponent<Health>();

                if (healthBehaviour != null)
                    healthBehaviour.ApplyDammage(m_value);
            }
        }
    }
}
