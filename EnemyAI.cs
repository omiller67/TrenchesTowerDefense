using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {


    public GameObject impactEffect;
    public float speed;

    public int health;

    public int value;

    private bool removeOneLife = false;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += value;
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
    }

    void Update()
    {

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if(transform.position.z >= -1)
        {
            if (!removeOneLife)
            {
                PlayerStats.Lives--;
                removeOneLife = true;
            }

        }
    }
}
