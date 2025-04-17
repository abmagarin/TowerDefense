using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (currentWaypointIndex >= waypoints.Length) return;

        Vector3 target = waypoints[currentWaypointIndex].position;

        // Solo movemos hacia XZ, pero respetamos la Y actual del enemigo
        Vector3 currentPos = transform.position;
        Vector3 targetXZ = new Vector3(target.x, currentPos.y, target.z);
        Vector3 direction = (targetXZ - currentPos).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(currentPos, targetXZ) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}
