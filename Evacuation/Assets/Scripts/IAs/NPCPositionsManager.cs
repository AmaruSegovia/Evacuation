

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Posiciones
{
    TORTUGA,
    FILA,
    SEGUIR,
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
                new Vector3(-3, 0, 0), // Más abajo
                new Vector3(-4, 0, 0)  // Mucho más abajo
            }
        }
    };

    void Update()
    {
        // Ejemplo: Cambiar entre TORTUGA y FILA al presionar las teclas "T" y "F"
        if (Input.GetKeyDown(KeyCode.T))
        {
            posicionActual = Posiciones.TORTUGA;
            ActualizarPosicionesNPCs();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            posicionActual = Posiciones.FILA;
            ActualizarPosicionesNPCs();
        }
    }


    // Método para actualizar las posiciones de los NPCs según el enum Posiciones
    public void ActualizarPosicionesNPCs()
    {
        if (posicionesDict.ContainsKey(posicionActual))
        {
            Vector3[] posiciones = posicionesDict[posicionActual];

            for (int i = 0; i < PosicionesNPC.Count && i < posiciones.Length; i++)
            {
                // Asignar la posición del diccionario al Transform correspondiente en localPosition
                PosicionesNPC[i].localPosition = posiciones[i];
            }

            // Notificar a los NPCs que las posiciones han cambiado
            NotificarCambioDePosicion();
        }
        else
        {
            Debug.LogWarning("La posición especificada no existe en el diccionario.");
        }
    }

    // Método para notificar a los NPCs sobre el cambio de posición
    private void NotificarCambioDePosicion()
    {
        NPCIA[] npcs = FindObjectsOfType<NPCIA>();  // Encontrar todos los NPCs en la escena

        foreach (var npc in npcs)
        {
            // Obtener el transform que corresponde a cada NPC y actualizarlo
            Transform nuevaPosicion = ObtenerTransformNPC(npc.numeroNPC);
            npc.AsignarTransformDestino(nuevaPosicion);
        }
    }

    // Método para obtener el Transform correspondiente al número de NPC
    public Transform ObtenerTransformNPC(int numeroNPC)
    {
        if (numeroNPC >= 0 && numeroNPC < PosicionesNPC.Count)
        {
            return PosicionesNPC[numeroNPC];
        }
        else
        {
            Debug.LogWarning("El número de NPC está fuera de rango.");
            return null;
        }
    }
}
