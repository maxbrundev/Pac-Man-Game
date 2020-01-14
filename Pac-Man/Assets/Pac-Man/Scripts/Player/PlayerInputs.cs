using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    /**
	* A class that encapsulates the Player Inputs
	*/
    public class PlayerInputs : MonoBehaviour
    {
        [Header("INPUTS BINDING")]
        [SerializeField] private string m_horizontalAxisInput;
        [SerializeField] private string m_verticalAxisInput;

        public Vector2 AxisInput
        {
            get
            {
                Vector2 input = Vector2.zero;
                input.x = Input.GetAxis(m_horizontalAxisInput);
                input.y = Input.GetAxis(m_verticalAxisInput);
                return input;
            }
        }

        public Vector2 AxisInputRaw
        {
            get
            {
                Vector2 input = Vector2.zero;
                input.x = Input.GetAxisRaw(m_horizontalAxisInput);
                input.y = Input.GetAxisRaw(m_verticalAxisInput);
                return input;
            }
        }
    }
}