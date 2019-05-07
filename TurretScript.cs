using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float rateOfFire = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public GameObject bulletPrefab;
    public Transform[] firePoints;

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(gameObject.tag == "Arty")
            {
                if (distanceToEnemy < shortestDistance && distanceToEnemy >= 30f)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
            else
            {
                if (distanceToEnemy < shortestDistance && enemy.transform.position.z <= 0)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update () {
		if(target == null)
        {
            return;
        }

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / rateOfFire;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject[] bullets = new GameObject[firePoints.Length];
        for (int i = 0; i < firePoints.Length; i++)
        {
            Transform firePosition = firePoints[i];
            GameObject tempBullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.LookRotation(target.position));
            bullets[i] = tempBullet;
        }
        foreach(GameObject bullet in bullets)
        {
            ExplosiveShell realShell;
            BulletScript realBullet;
            if (this.tag == "Arty")
            {
                realShell = bullet.GetComponent<ExplosiveShell>();
                if (realShell != null)
                {
                    realShell.Seek(target);
                }
            }
            else
            {
                realBullet = bullet.GetComponent<BulletScript>();
                if (realBullet != null)
                {
                    realBullet.Seek(target);
                }
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
