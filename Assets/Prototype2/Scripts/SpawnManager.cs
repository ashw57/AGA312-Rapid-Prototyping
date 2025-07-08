using UnityEngine;

namespace Prototype2
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject[] enemyPrefab;
        public GameObject[] pickupPrefab;

        private float spawnRange = 9.0f;

        public int enemyCount;
        public int waveNumber = 1;

        void Start()
        {
            SpawnEnemyWave(waveNumber);
            SpawnPickups(waveNumber);
        }

        void Update()
        {
            enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
            if (enemyCount == 0)
            {
                waveNumber++; 
                
                SpawnEnemyWave(waveNumber);
                SpawnPickups(waveNumber);
            }
        }

        void SpawnPickups(int pickupsToSpawn)
        {
            for (int i = 0; i < pickupsToSpawn; i++)
            {
                int randomPickup = Random.Range(0, pickupPrefab.Length);
                Instantiate(pickupPrefab[randomPickup], GenerateSpawnPosition(), Quaternion.identity);
            }
        }
        void SpawnEnemyWave(int enemiesToSpawn)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int randomEnemy = Random.Range(0, enemyPrefab.Length);

                Instantiate(enemyPrefab[randomEnemy], GenerateSpawnPosition(), enemyPrefab[randomEnemy].transform.rotation);
            }
        }

        private Vector3 GenerateSpawnPosition()
        {
            float spawnPosX = Random.Range(-spawnRange, spawnRange);
            float spawnPosZ = Random.Range(-spawnRange, spawnRange);

            Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

            return randomPos;
        }



    }
}
