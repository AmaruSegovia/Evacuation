using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;  // El jugador
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        // Busca el objeto del jugador con la etiqueta
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
            Debug.Log("Target asignado: " + target.name);
        }
        else
        {
            Debug.LogWarning("No se encontro un objeto con Tag 'Player'");
        }
    }

    void LateUpdate()
    {
        if (target != null){ 
            // Calcula la posici�n deseada solo en X e Y
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

            // Interpolaci�n suave entre la posici�n actual y la deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Aplicar la posici�n suavizada a la c�mara
            transform.position = smoothedPosition;
        }
    }
}
