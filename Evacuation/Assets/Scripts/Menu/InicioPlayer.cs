using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioPlayer : MonoBehaviour
{
    void Awake()
    {
        int indexPlayer = PlayerPrefs.GetInt("SelectedCharacterIndex");
        Instantiate(GameManager.Instance.characters[indexPlayer].characterJugable, transform.position, Quaternion.identity);
    }

}
