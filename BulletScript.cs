using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private Transform target;
    public GameObject impactEffect;

    public int damage = 5;

    public float speed = 70f;

    public void Seek(Transform seekTarget)
    {
        target = seekTarget;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(target == null)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            Destroy(gameObject, 1f);  
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            Destroy(gameObject);
            Damage(target);
            return;
        }

        transform.rotation = Quaternion.LookRotation(direction);
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void Damage(Transform enemy)
    {
        EnemyAI e = enemy.GetComponent<EnemyAI>();

        if(e != null)
        {
            e.TakeDamage(damage);
        }


    }
}
