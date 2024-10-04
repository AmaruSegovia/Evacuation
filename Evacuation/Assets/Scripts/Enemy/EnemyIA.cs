using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    private AIDestinationSetter destino;

    // Start is called before the first frame update
    void Start()
    {
        destino = GetComponent<AIDestinationSetter>();

        if (destino == null)
        {
            Debug.LogError("No se encontró una AIDestinationSetter en este GameObject.");
            return;
        }


        // Busca el objeto del jugador con la etiqueta "Player"
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Debug.Log("La cámara encontró al jugador!");
            // Asigna el Transform del jugador al campo Follow de la cámara
            destino.target = player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con Tag 'Player', atte: La cámara");
        }
    }
}
