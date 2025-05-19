using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float range = 1f;
    public Transform weaponPivot;

    private Transform target;
    public GameObject ammo;
    public float ammoSpam;
    private float fireCooldown = 0f;

    void Start()
    {
        if (weaponPivot == null)
        {
            if (gameObject.name.Contains("ballistaTower"))
            {
                weaponPivot = transform.Find("weapon-ballista");
            }
            else if (gameObject.name.Contains("cannonTower"))
            {
                weaponPivot = transform.Find("weapon-cannon");
            }
            else if (gameObject.name.Contains("turretTower"))
            {
                weaponPivot = transform.Find("weapon-turret");
            }

            if (weaponPivot == null)
            {
                Debug.LogError("weaponPivot no asignado");
            }
        }
    }
    void Update()
    {
        FindTarget();

        if (target != null)
        {
            Vector3 direction = target.position - weaponPivot.position;
            direction.y = 0f;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            weaponPivot.rotation = Quaternion.Lerp(weaponPivot.rotation, lookRotation, Time.deltaTime * 5f);
            fireCooldown -= Time.deltaTime;

            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = ammoSpam;
            }
        }
    }

    void FindTarget()
    {
        // Si ya hay un objetivo y sigue vivo y dentro del rango, no lo cambia
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= range)
            {
                return;
            }
            else
            {
                target = null; // Salió de rango
            }
        }

        // Buscar un nuevo enemigo más cercano dentro del rango
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue; // Por si algún enemigo ha sido destruido

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= range && distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy;
    }

    void Shoot()
    {
        if (!Application.isPlaying) return;

        Vector3 spawnPos = weaponPivot.position + weaponPivot.forward * 0.3f + Vector3.up * 0.2f;
        Vector3 direction = target.position - spawnPos + Vector3.up * 0.5f;
        Quaternion rotation = Quaternion.LookRotation(direction);

        Instantiate(ammo, spawnPos, rotation);
    }
}
