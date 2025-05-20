using UnityEngine;

public class SnowShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public float defaultDistance = 30f; // distancia por defecto si no se toca ningún collider

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && GameManager.Instance.snowSelected) // clic izquierdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint;

            // Si golpea algo con collider, usamos ese punto
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                // Si no, proyectamos el rayo a una distancia fija
                targetPoint = ray.origin + ray.direction * defaultDistance;
            }

            // Lanzar el proyectil desde este objeto (no la cámara)
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.AddComponent<ProjectileMover>().Init(targetPoint, projectileSpeed);

            GameManager.Instance.snowCooldown = false;
            GameManager.Instance.snowSelected = false;
            _ = GameManager.Instance.StartSnowCooldownTimerAsync();
        }
    }
}
