using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = InputManager.Instance.MovementInput;
        if (input != Vector2.zero)
        {
            moveDirection = new Vector3(input.x, 0, input.y) * speed;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        
        controller.Move(moveDirection * Time.deltaTime);
    }
}
