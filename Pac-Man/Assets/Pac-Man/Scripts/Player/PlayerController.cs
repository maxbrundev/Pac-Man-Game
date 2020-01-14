using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetComponents()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void InitComponents()
    {

    }
}
