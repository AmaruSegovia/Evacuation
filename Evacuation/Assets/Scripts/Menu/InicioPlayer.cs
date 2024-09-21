using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioPlayer : MonoBehaviour
{
    void Awake()
    {
        if(PlayerPrefs.GetInt("SelectedCharacterIndex") == 5){
            // Obtener un valor aleatorio entre los personajes de la lista menos el random.
            int index = Random.Range(0,GameManager.Instance.characters.Count-1);
            Debug.Log("aqui hay un random   " + index);
            // Asignar al prefab del indice del pj aleatorio
            PlayerPrefs.SetInt("SelectedCharacterIndex", index);
        }

        int indexPlayer = PlayerPrefs.GetInt("SelectedCharacterIndex");
        Instantiate(GameManager.Instance.characters[indexPlayer].characterJugable, transform.position, Quaternion.identity);
        
    }

}
