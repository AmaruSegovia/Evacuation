using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTransparency : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private int numberLayer = 2;
    [SerializeField] private int newNumberLayer = 3;
    private Material tilemapMaterial;
    private Color originalColor;
    private Color transparentColor;
    private TilemapRenderer tilemapRenderer;
    private Coroutine activeTransition;  // Referencia a la coroutine activa

    private void Start()
    {
        tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
        tilemapMaterial = tilemapRenderer.material;

        originalColor = tilemapMaterial.color;
        transparentColor = originalColor;
        transparentColor.a = 0.5f;  // Ajusta el valor de alpha para la transparencia
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tilemapRenderer.sortingOrder = newNumberLayer;
            // Si hay una transición en curso, detenerla antes de iniciar una nueva
            if (activeTransition != null)
                StopCoroutine(activeTransition);
            // Iniciar una nueva transición
            activeTransition = StartCoroutine(TransitionToColor(transparentColor));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tilemapRenderer.sortingOrder = numberLayer;
            // Si hay una transición en curso, detenerla antes de iniciar una nueva
            if (activeTransition != null)
                StopCoroutine(activeTransition);
            // Iniciar una nueva transición de vuelta al color original
            activeTransition = StartCoroutine(TransitionToColor(originalColor));
        }
    }

    private IEnumerator TransitionToColor(Color targetColor)
    {
        Color currentColor = tilemapMaterial.color;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            tilemapMaterial.color = Color.Lerp(currentColor, targetColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tilemapMaterial.color = targetColor;
        activeTransition = null;  // Marcar que la transición ha terminado
    }
}