using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    public string escenaACambiar;
    void Update()
    {

        if (Input.GetKey(KeyCode.P)) // "Fire1" es el click izquierdo del mouse
        {
            SceneManager.LoadScene(escenaACambiar);
            Debug.Log("Deberiua cambairse ");
        }
    }
}
