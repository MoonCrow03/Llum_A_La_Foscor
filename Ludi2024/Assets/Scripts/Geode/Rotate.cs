using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float m_RotationSpeed = 50.0f;

    private bool m_GameStarted;
    private bool m_IsRotating;
    private float m_StartMousePosition = 1.0f;

    public bool IsRotating => m_IsRotating;

    void Start()
    {
        m_GameStarted = false;
        m_IsRotating = false;
    }

    void Update()
    {
        if(!m_GameStarted) return;

        if (InputManager.Instance.RightClick.Tap)
        {
            m_IsRotating = true;
            m_StartMousePosition = InputManager.Instance.MousePosition.x;
        }

        if (InputManager.Instance.RightClick.Release)
        {
            m_IsRotating = false;
        }

        if (m_IsRotating)
        {
            float l_mouseDelta = InputManager.Instance.MousePosition.x - m_StartMousePosition;

            transform.Rotate(Vector3.forward, -l_mouseDelta * m_RotationSpeed * Time.deltaTime);

            m_StartMousePosition = InputManager.Instance.MousePosition.x;
        }
    }

    private void StartGame()
    {
        m_GameStarted = true;
    }

    private void OnEnable()
    {
        TutorialText.OnTutorialFinished += StartGame;
    }

    private void OnDisable()
    {
        TutorialText.OnTutorialFinished -= StartGame;
    }
}
