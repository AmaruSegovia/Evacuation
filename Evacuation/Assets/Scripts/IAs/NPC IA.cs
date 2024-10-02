using Pathfinding;
using UnityEngine;

public class NPCIA : MonoBehaviour
{
    private AIDestinationSetter destino;
    public int numeroNPC;
    private Transform posicionDestino;  // Transform de destino que será asignado

    void Start()
    {
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
}
