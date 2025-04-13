using UnityEngine;

public class Hover : MonoBehaviour
{
    public float hoverHeight = 0.5f;   // Amplitud del movimiento vertical
    public float hoverSpeed = 2f;      // Velocidad del movimiento
    private float baseY;

    void Start()
    {
        baseY = transform.position.y;

    }

    void Update()
    {
        // Calculamos la nueva posición Y usando una onda senoidal
        float newY = baseY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // Aplicamos la nueva posición manteniendo X y Z iguales
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
