using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoNPC
{
    Neutral,
    Ataque,
    Empuje
}

public class NPCAttack : MonoBehaviour
{
    public EstadoNPC estadoActual = EstadoNPC.Empuje;  // Estado actual del NPC
    public float radioDeteccion = 5f;  // Radio de detección del NPC
    public float fuerzaEmpuje = 5f;    // Fuerza de empuje en el modo Empuje

    private Transform objetivo;
    void Update()
    {
        switch (estadoActual)
        {
            case EstadoNPC.Neutral:
                // No hace nada en estado Neutral
                break;

            case EstadoNPC.Ataque:
                BuscarEnemigoCercano();
                if (objetivo != null)
                {
                    Debug.Log("NPC detectó a un enemigo en modo Ataque: " + objetivo.name);
                }
                break;

            case EstadoNPC.Empuje:
                // En este caso solo empuja si hay colisión, la lógica está en OnCollisionEnter2D
                break;
        }
    }

    // Método para buscar el enemigo más cercano dentro del radio de detección
    void BuscarEnemigoCercano()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("EnemyZombie");
        float distanciaMinima = Mathf.Infinity;
        Transform enemigoMasCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector2.Distance(transform.position, enemigo.transform.position);

            if (distancia < distanciaMinima && distancia <= radioDeteccion)
            {
                distanciaMinima = distancia;
                enemigoMasCercano = enemigo.transform;
            }
        }

        objetivo = enemigoMasCercano;  // Asignamos el enemigo más cercano como objetivo
    }

    // Detectar colisiones con los enemigos en el modo Empuje
    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (estadoActual == EstadoNPC.Empuje && colision.gameObject.CompareTag("EnemyZombie"))
        {
            Rigidbody2D rbEnemigo = colision.gameObject.GetComponent<Rigidbody2D>();
            AIPath aiPathEnemigo = colision.gameObject.GetComponent<AIPath>();  // AI del enemigo

            if (rbEnemigo != null)
            {
                // Calcula la dirección opuesta (del enemigo hacia afuera desde el NPC)
                Vector2 direccionEmpuje = (colision.transform.position - transform.position).normalized;

                // Invertir la dirección para que el empuje sea hacia afuera (alejando al enemigo)
                Vector2 direccionContraria = -direccionEmpuje;

                // Desactivar temporalmente el movimiento de la IA del zombie
                if (aiPathEnemigo != null)
                {
                    aiPathEnemigo.enabled = false;
                }

                // Aplicar la fuerza de empuje hacia afuera
                rbEnemigo.AddForce(direccionContraria * fuerzaEmpuje, ForceMode2D.Impulse);

                Debug.Log("NPC empujó a: " + colision.gameObject.name);
                
                // Reactivar el movimiento de la IA del zombie después de un pequeño retraso
                StartCoroutine(ReactivarMovimientoZombie(aiPathEnemigo));
            }
        }
    }

    // Coroutine para reactivar el movimiento de la IA del zombie después de un tiempo
    IEnumerator ReactivarMovimientoZombie(AIPath aiPathEnemigo)
    {
        yield return new WaitForSeconds(1f);  // Espera 0.5 segundos para reactivar el movimiento del zombie

        if (aiPathEnemigo != null)
        {
            aiPathEnemigo.enabled = true;
        }
    }

    // Método para cambiar el estado del NPC externamente
    public void CambiarEstado(EstadoNPC nuevoEstado)
    {
        estadoActual = nuevoEstado;
    }
}
