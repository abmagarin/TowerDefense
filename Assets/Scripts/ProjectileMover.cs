using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class ProjectileMover : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    [Header("Slow Effect Settings")]
    public float slowFactor = 0.5f;      // Reduce speed to 50%
    public float slowDuration = 3f;      // Effect lasts 3 seconds
    public float slowRange = 3f;         // Radius of effect

    public void Init(Vector3 target, float projectileSpeed)
    {
        direction = (target - transform.position).normalized;
        speed = projectileSpeed;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.y <= 0.2f)
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, slowRange);
        foreach (var collider in hitColliders)
        {
            EnemyWalk enemy = collider.GetComponent<EnemyWalk>();
            if (enemy != null)
            {
                StartCoroutine(SlowEnemy(enemy));
            }
        }

        Destroy(gameObject);
    }


    IEnumerator SlowEnemy(EnemyWalk enemy)
    {
        enemy.ApplySlow(slowFactor);
        WaitAndRemoveSlowAsync(enemy);
        yield break;
    }

    async void WaitAndRemoveSlowAsync(EnemyWalk enemy)
    {
        await Task.Delay((int)(slowDuration * 1000));
        if (enemy != null)
        {
            enemy.RemoveSlow();
        }
    }

}
