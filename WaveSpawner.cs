using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject tankPrefab;

    public Transform spawnPoint;

    public int waveSize;

    public float timeBetweenWaves;
    private float countdown = 15f;

    private int waveNumber = 0;

    public Text waveCountdownText;

    void Update()
    {
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        PlayerStats.Rounds++;
        for(int i = 0; i<= waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }

        waveNumber++;
    }

    void SpawnEnemy()
    {
        float randomX;
        Vector3 spawnPosition;
        for (int i = 0; i < waveSize; i++)
        {
            randomX = (float)Random.Range(-20 / 2, 20 / 2);
            spawnPosition = new Vector3(randomX, spawnPoint.position.y, spawnPoint.position.z);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

        if (waveNumber % 5 == 0)
        {

            int diceRoll = (int)Random.Range(0,2);
            if(diceRoll == 1)
            {
                randomX = (float)Random.Range(-20 / 2, 20 / 2);
                spawnPosition = new Vector3(randomX, spawnPoint.position.y + 1, spawnPoint.position.z);
                Instantiate(tankPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
