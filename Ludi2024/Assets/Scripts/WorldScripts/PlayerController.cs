using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dustParticles;

    private ParticleSystem.EmissionModule dustParticleEmission;

    public static bool IsMoving;
    
    private void Start()
    {
        transform.position = GameManager.Instance.m_StartPosition;
        transform.rotation = GameManager.Instance.m_StartRotation;
        dustParticleEmission = dustParticles.emission;

    }
    
    void Update()
    {
        // Obtener las entradas horizontales y verticales del jugador (WASD)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Si el jugador está moviéndose en alguna dirección
        if (direction.magnitude >= 0.1f)
        {
            // Emitir partículas set emitter rate to 3 
            dustParticleEmission.rateOverTime = 3;
            
            IsMoving = true;
            // Activar la animación de movimiento
            animator.SetBool("isMoving", true);
            
            // Calcular el ángulo hacia el que se debe rotar el personaje basado en la dirección de la cámara
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // Suavizar el giro del personaje
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Aplicar la rotación
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Mover al personaje en la dirección hacia la que está rotando
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            dustParticleEmission.rateOverTime = 0;
            IsMoving = false;
            // Desactivar la animación de movimiento
            animator.SetBool("isMoving", false);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnSetPlayerPosition += SetPlayerPosition;
    }
    
    private void OnDisable()
    {
        GameEvents.OnSetPlayerPosition -= SetPlayerPosition;
    }

    private void SetPlayerPosition()
    {
        GameManager.Instance.m_StartPosition = transform.position;
        GameManager.Instance.m_StartRotation = transform.rotation;
    }
}