using UnityEngine;

public class test : MonoBehaviour
{
    public HUD healthBar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Usa la barra espaciadora para recibir daño
        {
            healthBar.RecibirDaño(10f);
            Debug.Log("Daño: 10");
        }

        if (Input.GetKeyDown(KeyCode.H)) // Usa la tecla H para curar
        {
            healthBar.Curar(10f);
            Debug.Log("Curacion ");
        }
    }
}
