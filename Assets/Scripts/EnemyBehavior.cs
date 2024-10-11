using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRange;
    public float spawnInterval;
    private float spawnTimer;

    public void Start()
    {
        spawnTimer = spawnInterval;
    }
    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            //spawn enemy
            float randomAngle = Random.Range(0f, 360f);
            float x = Mathf.Cos(randomAngle)*spawnRange;
            float z = Mathf.Sin(randomAngle)*spawnRange;
            Vector3 pos = transform.position + new Vector3(x, 0.5f, z);
            Instantiate(enemyPrefab, pos, Quaternion.identity);

            spawnTimer += spawnInterval;
        }
    }
}
