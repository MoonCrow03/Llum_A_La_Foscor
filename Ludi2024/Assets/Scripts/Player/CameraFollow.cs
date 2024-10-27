using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;  // El transform del jugador
    [SerializeField] private float offsetX = 0f;  // Desplazamiento fijo en el eje X
    [SerializeField] private float offsetZ = 0f;  // Desplazamiento fijo en el eje Z
    [SerializeField] private float offsetY = 5f;  // Desplazamiento en el eje Y
    [SerializeField] private float smoothSpeed = 0.125f;  // Velocidad de suavizado

    [SerializeField] private bool m_CanMove;
    
    private void Start()
    {
        m_CanMove = true;
    }

    private void OnEnable()
    {
        GameEvents.OnEnablePlayerMovement += EnablePlayerMovement;
    }

    private void OnDisable()
    {
        GameEvents.OnEnablePlayerMovement -= EnablePlayerMovement;
    }

    private void EnablePlayerMovement(bool p_enable)
    {
        m_CanMove = p_enable;
    }

    private void LateUpdate()
    {
        if(!m_CanMove) return;

        Vector3 desiredPosition = new Vector3(offsetX + player.position.x , offsetY, offsetZ + player.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;
    }
}
