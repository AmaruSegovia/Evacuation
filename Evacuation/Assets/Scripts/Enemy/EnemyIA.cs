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
            Debug.LogError("No se encontr� una AIDestinationSetter en este GameObject.");
            return;
        }


        // Busca el objeto del jugador con la etiqueta "Player"
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Debug.Log("La c�mara encontr� al jugador!");
            // Asigna el Transform del jugador al campo Follow de la c�mara
            destino.target = player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontr� un objeto con Tag 'Player', atte: La c�mara");
        }
    }
}
