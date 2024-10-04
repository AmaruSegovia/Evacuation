using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public List<CharacterNPC> npcDataList;  // Lista de ScriptableObjects NPC
    public NPCPositionsManager positionsManager;  // Referencia al NPCPositionsManager para obtener las posiciones

    void Start()
    {
        BuscarPositionManager();
        InstanciarNPCs();
    }

    void BuscarPositionManager()
    {
        // Busca el objeto con el tag "Player" en la escena
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Busca el componente NPCPositionsManager en los hijos del Player
            positionsManager = player.GetComponentInChildren<NPCPositionsManager>();

            if (positionsManager == null)
            {
                Debug.LogError("NPCPositionsManager no se encontró en el jugador.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el jugador en la escena.");
        }
    }

    void InstanciarNPCs()
    {
        if (positionsManager == null)
        {
            Debug.LogError("NPCPositionsManager no asignado.");
            return;
        }

        // Asegurarse de que hay suficientes posiciones para todos los NPCs
        if (positionsManager.PosicionesNPC.Count < npcDataList.Count)
        {
            Debug.LogError("No hay suficientes posiciones para todos los NPCs");
            return;
        }

        // Iterar sobre la lista de NPCData y crear NPCs
        for (int i = 0; i < npcDataList.Count; i++)
        {
            // Instanciar el prefab en la posición correspondiente
            GameObject npc = Instantiate(npcDataList[i].characterJugable, positionsManager.PosicionesNPC[i].position, Quaternion.identity);
            // Obtener el componente NPCIA y asignarle el ScriptableObject
            NPCIA npcIA = npc.GetComponent<NPCIA>();
            if (npcIA != null)
            {
                npcIA.numeroNPC = i;
                // Asignar el transform de destino desde el NPCPositionsManager
                Transform transformDestino = positionsManager.ObtenerTransformNPC(i);
                npcIA.AsignarTransformDestino(transformDestino);
            }
        }
    }
}
