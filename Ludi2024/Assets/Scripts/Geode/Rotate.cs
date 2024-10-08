using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float m_RotationSpeed = 50.0f;

    private bool m_IsRotating;
    private float m_StartMousePosition = 1.0f;



    void Start()
    {
        m_IsRotating = false;
    }

    void Update()
    {
        if (InputManager.Instance.LeftClick.Tap)
        {
            m_IsRotating = true;
            m_StartMousePosition = InputManager.Instance.MousePosition.x;
        }

        if (InputManager.Instance.LeftClick.Release)
        {
            m_IsRotating = false;
        }

        if (m_IsRotating)
        {
            float l_mouseDelta = InputManager.Instance.MousePosition.x - m_StartMousePosition;

            transform.Rotate(Vector3.up, -l_mouseDelta * m_RotationSpeed * Time.deltaTime);

            m_StartMousePosition = InputManager.Instance.MousePosition.x;
        }
    }
}
