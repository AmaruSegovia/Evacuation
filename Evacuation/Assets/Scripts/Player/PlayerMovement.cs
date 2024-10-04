using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField]
    private float speed = 5.0f;  // Velocidad máxima

    [SerializeField]
    private float acceleration = 10.0f;  // Qué tan rápido alcanza la velocidad máxima
    [SerializeField]
    private float deceleration = 10.0f;  // Qué tan rápido se detiene

    [SerializeField]
    private Rigidbody2D rb;

    private int posicionNPcs;

    private Vector2 movementInput;
    private Vector2 currentVelocity;

    void Update()
    {
        // Obtener entrada del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Almacenar la dirección en un vector2
        movementInput = new Vector2(horizontalInput, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        // Si hay movimiento input, aceleramos hacia la velocidad deseada
        if (movementInput != Vector2.zero)
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, movementInput * speed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Si no hay input, desaceleramos
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        // Aplicar la velocidad al Rigidbody2D
        rb.velocity = currentVelocity;
    }
}
