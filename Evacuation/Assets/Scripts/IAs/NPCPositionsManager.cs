using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Posiciones
{
    TORTUGA,
    FILA,
    JUNTOS,
    CUADRADO,
    DETENERSE
}

public class NPCPositionsManager : MonoBehaviour
{
    public List<Transform> PosicionesNPC;
    public Posiciones posicionActual = Posiciones.TORTUGA;

    private Dictionary<Posiciones, Vector3[]> posicionesDict = new Dictionary<Posiciones, Vector3[]>
    {
        {
            Posiciones.TORTUGA, new Vector3[]
            {
                new Vector3(0, 1, 0),  // Arriba
                new Vector3(0, -1, 0), // Abajo
                new Vector3(-1, 0, 0), // Izquierda
                new Vector3(1, 0, 0)   // Derecha
            }
        },
        {
            Posiciones.FILA, new Vector3[]
            {
                new Vector3(-1, 0, 0),  // Arriba
                new Vector3(-2, 0, 0), // Abajo
                new Vector3(-3, 0, 0), // M�s abajo
                new Vector3(-4, 0, 0)  // Mucho m�s abajo
            }

        },
        {
            Posiciones.JUNTOS, new Vector3[]
            {
                Vector3.zero,  // Todos juntos
                Vector3.zero,
                Vector3.zero,
                Vector3.zero
            }
        },
        {
            Posiciones.CUADRADO, new Vector3[]
            {
                new Vector3(-1, 1, 0),  // Arriba izquierda
                new Vector3(-1, -1, 0), // Abajo izquierda
                new Vector3(1, 1, 0),   // Arriba derecha
                new Vector3(1, -1, 0)   // Abajo derecha
            }
        },
        {
            Posiciones.DETENERSE, new Vector3[]
            {
                Vector3.zero,  // Todos detenidos
                Vector3.zero,
                Vector3.zero,
                Vector3.zero
            }
        }
    };

    void Update()
    {
        // Ejemplo: Cambiar entre TORTUGA, FILA, JUNTOS, CUADRADO, DETENERSE
        if (Input.GetKeyDown(KeyCode.T))
        {
            posicionActual = Posiciones.TORTUGA;
            ActualizarPosicionesNPCs();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            posicionActual = Posiciones.FILA;
            ActualizarPosicionesNPCs();
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            posicionActual = Posiciones.JUNTOS;
            ActualizarPosicionesNPCs();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            posicionActual = Posiciones.CUADRADO;
            ActualizarPosicionesNPCs();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            posicionActual = Posiciones.DETENERSE;
            ActualizarPosicionesNPCs();
        }
    }

    // M�todo para actualizar las posiciones de los NPCs seg�n el enum Posiciones
    public void ActualizarPosicionesNPCs()
    {
        if (posicionesDict.ContainsKey(posicionActual))
        {
            Vector3[] posiciones = posicionesDict[posicionActual];

            for (int i = 0; i < PosicionesNPC.Count && i < posiciones.Length; i++)
            {
                // Asignar la posici�n del diccionario al Transform correspondiente en localPosition
                PosicionesNPC[i].localPosition = posiciones[i];
            }

            // Notificar a los NPCs que las posiciones han cambiado
            NotificarCambioDePosicion();
        }
        else
        {
            Debug.LogWarning("La posici�n especificada no existe en el diccionario.");
        }
    }

    // M�todo para notificar a los NPCs sobre el cambio de posici�n
    private void NotificarCambioDePosicion()
    {
        NPCIA[] npcs = FindObjectsOfType<NPCIA>();  // Encontrar todos los NPCs en la escena

        foreach (var npc in npcs)
        {
            if (posicionActual == Posiciones.DETENERSE)
            {
                // Si la posici�n actual es DETENERSE, detener a todos los NPCs
                npc.DetenerNPC();
            }
            else
            {
                // Obtener el transform que corresponde a cada NPC y actualizarlo
                Transform nuevaPosicion = ObtenerTransformNPC(npc.numeroNPC);
                npc.AsignarTransformDestino(nuevaPosicion);
                npc.ReanudarNPC(); // Reanudar movimiento si no est�n en DETENERSE
            }
        }
    }

    // M�todo para obtener el Transform correspondiente al n�mero de NPC
    public Transform ObtenerTransformNPC(int numeroNPC)
    {
        if (numeroNPC >= 0 && numeroNPC < PosicionesNPC.Count)
        {
            return PosicionesNPC[numeroNPC];
        }
        else
        {
            Debug.LogWarning("El n�mero de NPC est� fuera de rango.");
            return null;
        }
    }
}
