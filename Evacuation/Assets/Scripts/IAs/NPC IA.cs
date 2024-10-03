using Pathfinding;
using System.IO;
using UnityEngine;

public class NPCIA : MonoBehaviour
{
    private AIDestinationSetter destino;
    public int numeroNPC;
    private Transform posicionDestino;  // Transform de destino que será asignado
    private AIPath aiPath;
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destino = GetComponent<AIDestinationSetter>();
        if (posicionDestino != null)
        {
            destino.target = posicionDestino;
        }
        else
        {
            Debug.LogWarning("No se ha asignado un transform de destino para este NPC.");
        }
    }

    // Método para asignar el transform de destino desde el NPCManager
    public void AsignarTransformDestino(Transform transformDestino)
    {
        posicionDestino = transformDestino;
        if (destino != null)
        {
            destino.target = posicionDestino;
        }
    }

    // Método para detener al NPC (desactivar el movimiento)
    public void DetenerNPC()
    {
        if (aiPath != null)
        {
            aiPath.canMove = false; // Esto detiene al NPC
        }
    }

    // Método para reanudar el movimiento del NPC
    public void ReanudarNPC()
    {
        if (aiPath != null)
        {
            aiPath.canMove = true; // Esto reactiva el movimiento del NPC
        }
    }
}
