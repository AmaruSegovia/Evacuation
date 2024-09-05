using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class FlashlightController : MonoBehaviour
{
    private Vector3 objetivo;
    [SerializeField] private Camera camara;
    [SerializeField] private float smoothSpeed = 5.0f;  // Velocidad de suavizado
    private Light2D linternaLuz;  // Referencia al componente Light2D
    private bool linternaEncendida = true;  // Estado de la linterna
    void Start()
    {
        // Obtener la referencia al componente Light2D
        linternaLuz = GetComponent<Light2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))    
        {
            linternaEncendida = !linternaEncendida;
            linternaLuz.enabled = linternaEncendida;  // Encender o apagar la luz
        }

        // Solo rotar la linterna si est� encendida
        if (linternaEncendida)
        {
            // Obtener la posici�n del mouse en el mundo 2D
            objetivo = camara.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camara.nearClipPlane));
            objetivo.z = 0f;

            // Calcular el �ngulo deseado para la linterna
            float anguloRadianes = Mathf.Atan2(objetivo.y - transform.position.y, objetivo.x - transform.position.x);
            float anguloGrados = anguloRadianes * Mathf.Rad2Deg - 90f;

            // Obtener la rotaci�n actual de la linterna
            Quaternion rotacionActual = transform.rotation;

            // Calcular la rotaci�n deseada
            Quaternion rotacionDeseada = Quaternion.Euler(0f, 0f, anguloGrados);

            // Interpolar suavemente entre la rotaci�n actual y la deseada
            transform.rotation = Quaternion.Lerp(rotacionActual, rotacionDeseada, smoothSpeed * Time.deltaTime);
        }
    }
}

