using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera camara;

    void Start()
    {
        // Obtiene la referencia de la CinemachineVirtualCamera en el mismo GameObject
        camara = GetComponent<CinemachineVirtualCamera>();

        if (camara == null)
        {
            Debug.LogError("No se encontró una CinemachineVirtualCamera en este GameObject.");
            return;
        }

        // Busca el objeto del jugador con la etiqueta "Player"
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Debug.Log("La cámara encontró al jugador!");
            // Asigna el Transform del jugador al campo Follow de la cámara
            camara.Follow = player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con Tag 'Player', atte: La cámara");
        }
    }
}
