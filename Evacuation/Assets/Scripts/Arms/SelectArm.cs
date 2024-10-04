using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectArm : MonoBehaviour
{
    public List<Arms> armas; // Lista de objetos Arms
    public Image imagenHUD; // Imagen del HUD que cambia según el arma seleccionada
    public TextMeshProUGUI balasText; // Texto para mostrar las balas

    private int indiceArmaActual = 0; // Índice del arma seleccionada
    
    void Start()
    {
        ActualizarHUD();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CambiarArma(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CambiarArma(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RecargarArma();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
    }

    private void CambiarArma(int direccion)
    {
        indiceArmaActual = (indiceArmaActual + direccion + armas.Count) % armas.Count; // Cambio circular de arma
        ActualizarHUD();
    }

    private void ActualizarHUD()
    {
        Arms armaActual = armas[indiceArmaActual];

        // Actualiza la imagen del HUD
        if (imagenHUD != null)
        {
            imagenHUD.sprite = armaActual.imagenArma;
        }

        if (balasText != null)
        {
            balasText.text = armaActual.balasRecargadas.ToString() + "/" + armaActual.balasMaximas.ToString();
        }
    }
    
    private void RecargarArma()
    {
        Arms armaActual = armas[indiceArmaActual];

        // Solo recarga si las balas recargadas son menores que las máximas
        if (armaActual.balasRecargadas < armaActual.balasMaximas)
        {
            armaActual.balasRecargadas = armaActual.balasMaximas; // Recarga a máximo
            ActualizarHUD();
        }
    }

    private void Disparar()
    {
        Arms armaActual = armas[indiceArmaActual];

        // Solo dispara si hay balas recargadas
        if (armaActual.balasRecargadas > 0)
        {
            armaActual.balasRecargadas--; // Disminuye la cantidad de balas recargadas
            ActualizarHUD();
        }
        else
        {
            Debug.Log("No hay balas para disparar.");
        }
    }
}
