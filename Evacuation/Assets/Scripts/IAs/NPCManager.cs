using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public List<CharacterNPC> npcDataList;  // Lista de ScriptableObjects NPC
    public NPCPositionsManager positionsManager;  // Referencia al NPCPositionsManager para obtener las posiciones

    void Start()
    {
        InstanciarNPCs();
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
            // Instanciar el prefab en la posici�n correspondiente
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
