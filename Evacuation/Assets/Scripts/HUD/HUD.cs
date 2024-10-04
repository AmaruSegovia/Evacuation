using System.Collections; // Agregado para las corutinas
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image imagenBarraDeVida; 
    public Color colorDeDaño; 
    public Color colorSaludable = Color.green; // Color para vida plena
    public Color colorMedio = Color.yellow; // Color para vida media (50)
    public Color colorBajo = Color.red; // Color para vida baja (20)
    public float vidaMaxima; 
    private float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima; 
        ActualizarBarraDeVida();

        // Establece la escala inicial de la barra de vida en el eje X
        RectTransform rectTransform = imagenBarraDeVida.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1.8f, 0.25f, 1f); // Escala inicial de 1.8 en X
    }

    public void RecibirDaño(float daño)
    {
        vidaActual = Mathf.Max(0, vidaActual - daño);
        StartCoroutine(ActualizarBarraDeVidaSuavemente());
        StartCoroutine(FlashColorDeDaño());
    }

    public void Curar(float cantidadDeCuracion)
    {
        vidaActual = Mathf.Min(vidaMaxima, vidaActual + cantidadDeCuracion);
        StartCoroutine(ActualizarBarraDeVidaSuavemente());
    }

    private void ActualizarBarraDeVida()
    {
        if (imagenBarraDeVida)
        {
            float cantidadLlenado = vidaActual / vidaMaxima; 
            imagenBarraDeVida.fillAmount = cantidadLlenado; 

            // Ajusta la escala de la barra de vida según la vida actual
            RectTransform rectTransform = imagenBarraDeVida.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1.8f * cantidadLlenado, 0.25f, 1f); // Cambia la escala en función de la vida actual
        }
        else
        {
            Debug.LogError("La imagen de la barra de vida no está asignada.");
        }
    }

    private IEnumerator ActualizarBarraDeVidaSuavemente()
    {
        float targetFillAmount = vidaActual / vidaMaxima;
        float duration = 0.5f; 
        float elapsedTime = 0f;

        float initialScale = imagenBarraDeVida.rectTransform.localScale.x; // Escala inicial en X
        float targetScale = targetFillAmount * 1.8f; // Escala basado en vida actual

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            
            // fillAmount
            imagenBarraDeVida.fillAmount = Mathf.Lerp(imagenBarraDeVida.fillAmount, targetFillAmount, t);
            
            // escala
            float currentScale = Mathf.Lerp(initialScale, targetScale, t);
            imagenBarraDeVida.rectTransform.localScale = new Vector3(currentScale, 0.25f, 1f);

            yield return null; 
        }

        imagenBarraDeVida.fillAmount = targetFillAmount; 
        imagenBarraDeVida.rectTransform.localScale = new Vector3(targetScale, 0.25f, 1f);

        
            // Cambia el color de la barra de vida depende la vida actual
            if (vidaActual <= vidaMaxima/4)
            {
                imagenBarraDeVida.color = colorBajo; // Rojo
            }
            else if (vidaActual <= vidaMaxima/2)
            {
                imagenBarraDeVida.color = colorMedio; // Amarillo
            }
            else
            {
                imagenBarraDeVida.color = colorSaludable; // Verde
            }

            Debug.Log("Vida Player "+vidaActual);
    }
    private IEnumerator FlashColorDeDaño()
    {
        Color originalColor = imagenBarraDeVida.color;
        imagenBarraDeVida.color = colorDeDaño; 
        yield return new WaitForSeconds(0.1f); 
        imagenBarraDeVida.color = originalColor; 
    }
}
