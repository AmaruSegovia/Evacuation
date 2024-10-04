using UnityEngine;

[CreateAssetMenu(fileName = "NuevaArma", menuName = "Arms")]
public class Arms : ScriptableObject
{
    public Sprite imagenArma; // Imagen del arma
    public string nombreArma; // Nombre del arma
    public int daño; // Daño del arma
    public int balasMaximas; // Máximo de balas en mano
    public int balasRecargadas; // Máximo de balas recargadas
    public float tiempoDeRecarga; // Tiempo de recarga
}