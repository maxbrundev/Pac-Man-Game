using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform m_exitPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Teleport(other.gameObject);
        }
    }

    public void Teleport(GameObject p_object)
    {
        p_object.transform.localPosition = m_exitPosition.position;
    }
}
