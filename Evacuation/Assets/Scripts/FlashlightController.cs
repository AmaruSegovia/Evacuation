using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    private Vector3 objetivo;
    private Camera camara;
    [SerializeField] private float smoothSpeed = 5.0f;  // Velocidad de suavizado
    private Light2D linternaLuz;  // Referencia al componente Light2D
    private bool linternaEncendida = true;  // Estado de la linterna
    [SerializeField] private float titileoDuracion = 2.0f;  // Duraci�n del titileo inicial
    [SerializeField] private float titileoFrecuenciaMin = 0.05f;  // Frecuencia m�nima del titileo
    [SerializeField] private float titileoFrecuenciaMax = 0.2f;   // Frecuencia m�xima del titileo

    void Start()
    {
        camara = Camera.main;
        // Obtener la referencia al componente Light2D
        linternaLuz = GetComponent<Light2D>();
    }

    void Update()
    {
        if(camara == null){
            Debug.Log("error, no se encontro la camara");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            linternaEncendida = !linternaEncendida;
            if (linternaEncendida)
            {
                // Iniciar la corrutina para el titileo
                StartCoroutine(TitilarLinterna());
            }
            else
            {
                // Apagar la linterna
                StopAllCoroutines();  // Detener cualquier titileo activo
                linternaLuz.enabled = false;
            }
        }

        // Solo rotar la linterna si est� encendida
        if (linternaEncendida && linternaLuz.enabled)
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

    private IEnumerator TitilarLinterna()
    {
        // Simular titileo inicial durante un tiempo
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < titileoDuracion)
        {
            linternaLuz.enabled = Random.value > 0.5f;  // Encender o apagar aleatoriamente
            float tiempoEspera = Random.Range(titileoFrecuenciaMin, titileoFrecuenciaMax);  // Espera aleatoria entre titileos
            tiempoTranscurrido += tiempoEspera;
            yield return new WaitForSeconds(tiempoEspera);
        }

        // Despu�s del titileo inicial, mantener la linterna encendida o fallar ocasionalmente
        while (true)
        {
            linternaLuz.enabled = Random.value > 0.1f;  // Simular fallos ocasionales (10% de probabilidad de apagarse)
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));  // Cambiar el estado cada cierto tiempo aleatorio
        }
    }
}
