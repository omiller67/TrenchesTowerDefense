using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveShell : MonoBehaviour {

    private Transform target;
    public GameObject explosionEffect;
    public GameObject bloodEffect;

    public float speed = 70f;
    public float blastRadius = 10f;
    public int damage = 30;

    public void Seek(Transform seekTarget)
    {
        target = seekTarget;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {

            Destroy(gameObject);
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            Detonate();
            return;
        }

        transform.rotation = Quaternion.LookRotation(direction);
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }


    void Detonate()
    {
        Destroy(gameObject);
        GameObject effectIns = (GameObject)Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (blastRadius >= Vector3.Distance(transform.position, enemy.transform.position))
            {
                Damage(enemy.transform);
            }
        }
        Destroy(effectIns, 1f);

    }

    void Damage(Transform enemy)
    {
        EnemyAI e = enemy.GetComponent<EnemyAI>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }


    }
}
