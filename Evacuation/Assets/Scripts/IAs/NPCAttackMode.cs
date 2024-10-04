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
    public float radioDeteccion = 5f;  // Radio de detecci�n del NPC
    public float fuerzaEmpuje = 5f;    // Fuerza de empuje en el modo Empuje

    private Transform objetivo;
    private Transform jugador;  // Referencia al jugador

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            jugador = player.transform;
        }
        else
        {
            Debug.LogError("No se encontr� un objeto con el tag 'Player'");
        }
    }
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
                    Debug.Log("NPC detect� a un enemigo en modo Ataque: " + objetivo.name);
                }
                break;

            case EstadoNPC.Empuje:
                // En este caso solo empuja si hay colisi�n, la l�gica est� en OnCollisionEnter2D
                break;
        }
    }

    // M�todo para buscar el enemigo m�s cercano dentro del radio de detecci�n
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

        objetivo = enemigoMasCercano;  // Asignamos el enemigo m�s cercano como objetivo
    }

    // Detectar colisiones con los enemigos en el modo Empuje
    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (estadoActual == EstadoNPC.Empuje && colision.gameObject.CompareTag("EnemyZombie"))
        {
            Rigidbody2D rbEnemigo = colision.gameObject.GetComponent<Rigidbody2D>();
            AIPath aiPathEnemigo = colision.gameObject.GetComponent<AIPath>();  // IA del enemigo

            if (rbEnemigo != null && jugador != null)
            {
                // Calcula la direcci�n desde el jugador hacia el zombie (para empujar en direcci�n opuesta al jugador)
                Vector2 direccionEmpuje = (colision.transform.position - jugador.position).normalized;

                // Desactivar temporalmente el movimiento de la IA del zombie
                if (aiPathEnemigo != null)
                {
                    aiPathEnemigo.enabled = false;
                }

                // Aplicar la fuerza de empuje en la direcci�n opuesta al jugador
                rbEnemigo.AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);

                Debug.Log("NPC empuj� a: " + colision.gameObject.name);

                // Reactivar el movimiento de la IA del zombie despu�s de un peque�o retraso
                StartCoroutine(ReactivarMovimientoZombie(aiPathEnemigo));
            }
        }
    }

    // Coroutine para reactivar el movimiento de la IA del zombie despu�s de un tiempo
    IEnumerator ReactivarMovimientoZombie(AIPath aiPathEnemigo)
    {
        yield return new WaitForSeconds(0.5f);  // Espera 0.5 segundos para reactivar el movimiento del zombie

        if (aiPathEnemigo != null)
        {
            aiPathEnemigo.enabled = true;
        }
    }

    // M�todo para cambiar el estado del NPC externamente
    public void CambiarEstado(EstadoNPC nuevoEstado)
    {
        estadoActual = nuevoEstado;
    }
}
