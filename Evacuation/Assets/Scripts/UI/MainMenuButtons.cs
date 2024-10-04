using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lejos el peor código que alguna vez escribí
public class MainMenuButtons : MonoBehaviour
{
    public GameObject menu, creditos, intro, introUno, introDos, introTres, introCuatro;
    private Boolean isInIntro = false;
    private int pantallaIntro = 0;

    private void Update()
    {
        // avanzando en la intro
        if (isInIntro)
        {
            CheckPantallaIntro();
            if (Input.GetMouseButtonDown(0) && isInIntro)
            {
                if(pantallaIntro == 4)
                {

                }
                else
                {
                    pantallaIntro++;
                }
            }
        }
    }

    public void IniciarJuego()
    {
        menu.SetActive(false);
        intro.SetActive(true);
        isInIntro = true;
        pantallaIntro = 1;
    }

    public void AbrirCreditos()
    {
        menu.SetActive(false);
        creditos.SetActive(true);
    }

    public void VolverAlMenu()
    {
        menu.SetActive(true);
        creditos.SetActive(false);
    }

    public void CheckPantallaIntro()
    {
        switch(pantallaIntro)
        {
            case 1: 
                introUno.SetActive(true);
                break;
            case 2:
                introUno.SetActive(false);
                introDos.SetActive(true);
                break;
            case 3:
                introDos.SetActive(false);
                introTres.SetActive(true);
                break;
            case 4:
                introTres.SetActive(false);
                introCuatro.SetActive(true);
                break;
        }
    }
}
