using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab; // Prefab de la bala a instanciar
    [SerializeField] Transform firePoint; // El punto desde donde saldrá la bala
    [SerializeField] float bulletSpeed = 10f; // Velocidad de la bala
    private float distanceFromPlayer = 0.3f; // Distancia fija entre el jugador y el arma

    void Update()
    {
        RotateWeapon();

        if (Input.GetMouseButtonDown(0)) // "Fire1" es el click izquierdo del mouse
        {
            Debug.Log("Disparaste!!!!");
            Shoot();
        }
    }

    void RotateWeapon()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Ajustamos el valor de z a 0 porque estamos en 2D

        // Calcular la dirección entre el jugador y el mouse
        Vector3 direction = (mousePos - transform.parent.position).normalized;

        // Mover el arma a una distancia fija del jugador
        transform.position = transform.parent.position + direction * distanceFromPlayer;

        // Calcular el ángulo de rotación entre el jugador y el mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotar el arma
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Hacer flip en el eje Y cuando el mouse esté detrás del jugador
        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, -1, 1); // Invertir escala en Y
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Volver a la escala normal
        }
    }

    void Shoot()
    {
        // Instanciar la bala en el firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Obtener el componente Rigidbody2D para aplicar fuerza
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Aplicar una fuerza en la dirección del arma para que la bala se mueva
        rb.velocity = firePoint.right * bulletSpeed;
    }
}
