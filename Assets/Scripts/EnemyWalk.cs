using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyWalk : MonoBehaviour
{
    public Transform[] waypoints;
    private float baseSpeed;
    public float speed = 2f;

    public float hoverHeight = 0.5f;
    public float hoverSpeed = 2f;

    private int currentWaypointIndex = 0;
    private Rigidbody rb;
    private float baseY;
    private float randomOffset;
    public float life = 100;
    public float maxLife = 100;


    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        baseSpeed = speed;

        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Start()
    {
        baseY = transform.position.y;
        GameObject[] arrow = GameObject.FindGameObjectsWithTag("Arrow");
    }

    void Update()
    {
        if (life <= 0)
        {
            GameManager.Instance.AddCoins(5);
            Destroy(gameObject);
        }
    }

    public void SetWaypoints(Transform newWaypoints)
    {
        int waypointNumber = newWaypoints.childCount;
        waypoints = new Transform[waypointNumber];

        for (int i = 0; i < waypointNumber; i++)
        {
            waypoints[i] = newWaypoints.GetChild(i);
        }
    }

    void FixedUpdate()
    {
        if (currentWaypointIndex >= waypoints.Length) return;

        Vector3 currentPos = transform.position;
        Vector3 target = waypoints[currentWaypointIndex].position;
        Vector3 targetXZ = new Vector3(target.x, currentPos.y, target.z);
        Vector3 direction = (targetXZ - currentPos).normalized;

        // Movimiento lineal
        Vector3 nextXZ = currentPos + direction * speed * Time.deltaTime;

        // Movimiento vertical tipo hover
        float hoverY = baseY + Mathf.Sin(Time.time * hoverSpeed + randomOffset) * hoverHeight;

        // Combinamos XZ + hover
        Vector3 finalPosition = new Vector3(nextXZ.x, hoverY, nextXZ.z);
        rb.MovePosition(finalPosition);

        if (Vector3.Distance(currentPos, targetXZ) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow")) life -= 8;
        if (other.CompareTag("Bullet")) life -= 3;
        if (other.CompareTag("CannonBall")) life -= 18;
        if (other.CompareTag("finish"))
        {
            if (gameObject.name.StartsWith("ufo 3"))
            {
                GameManager.Instance.AddLives(-3);
            }
            else
            {
                GameManager.Instance.AddLives(-1);
            }

            Destroy(gameObject);
        }
    }
    public void ApplySlow(float factor)
    {
        speed = baseSpeed * factor;
    }

    public void RemoveSlow()
    {
        speed = baseSpeed;
    }



}
