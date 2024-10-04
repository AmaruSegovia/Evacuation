using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime = 2f; // Tiempo antes de que la bala se destruya automáticamente

    private void Start()
    {
        // Destruir la bala después de un tiempo si no colisiona
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Verificar si el objeto tiene el componente Enemy
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }

        // Destruir la bala al colisionar
        Destroy(gameObject);
    }
}
