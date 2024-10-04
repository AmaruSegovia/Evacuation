using UnityEngine;
using TMPro; // Importar la librería de TextMeshPro
using System.Collections; 

public class NPCLifes : MonoBehaviour
{
    public int lifes = 100;
    public int maxLifes = 100;
    public int damage = 10;
    public TextMeshProUGUI lifesText;
    private SpriteRenderer spriteRenderer;  // Referencia al Renderer

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        // Buscar el TextMeshProUGUI basándose en el nombre del NPC
        string textObjectName = "LifesText " + gameObject.name;  // Genera el nombre a buscar
        Debug.Log("game object :"+gameObject.name+" eso");
        Debug.Log("NOMBRE esperado :"+"name " + gameObject.name+":");
        GameObject textObject = GameObject.Find(textObjectName);  // Busca el objeto de texto en la escena

        if (textObject != null)
        {
            lifesText = textObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.Log("No existe el Text");
        }
        UpdateLifesText();
    }

    void Update()
    {
        // Reducir vida al presionar barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeLifes(-damage);
        }

        // Aumentar vida al presionar "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeLifes(damage);
        }
    }

    void ChangeLifes(int amount)
    {
        lifes = Mathf.Clamp(lifes + amount, 0, maxLifes);  // Limita entre 0 y el máximo de vida
        
        Debug.Log("NPC Vida: " + lifes);
        UpdateLifesText();

        // Si la vida llega a 0, iniciar la animación de desvanecimiento y destruir el NPC
        if (lifes <= 0)
        {
            StartCoroutine(FadeAndDestroy());
        }
    }

    // Función que actualiza el texto de la vida
    private void UpdateLifesText()
    {
        if (lifesText != null)
        {
            lifesText.text = "Vida: " + lifes;
        }
    }

    // Detecta colisiones con otros objetos
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si colisiona con el objeto llamado "Target Zombie"
        if (collision.gameObject.CompareTag("EnemyZombie"))
        {
            ChangeLifes(-damage);
        }
    }

    // Corutina que realiza el efecto de desvanecimiento
    IEnumerator FadeAndDestroy()
    {
        float elapsedTime = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / 1f);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            spriteRenderer.color = newColor;

            yield return null;
        }

        // Destruir el NPC
        Destroy(gameObject);
    }
}
