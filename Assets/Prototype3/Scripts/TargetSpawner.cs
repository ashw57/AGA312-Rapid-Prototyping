using System.Collections;
using System.Linq;
using UnityEngine;

namespace Prototype3
{
    public class TargetSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] targetPrefabs;
        [SerializeField] GameObject[] spawnPositions;
        [SerializeField] Transform[] wayPoints;


        [SerializeField] int targetsPerWave = 5;
        [SerializeField] int waypointCountPerTarget = 5;
        [SerializeField] float spawnDelay = 1.0f;
        [SerializeField] int totalWaves = 3;

        private void Start()
        {
            StartCoroutine(SpawnWaves());
        }

        private IEnumerator SpawnWaves()
        {
            int waveCount = 0;

            while (totalWaves < 0 || waveCount < totalWaves)
            {
                GameObject[] spawnedTargets = new GameObject[targetsPerWave];

                for (int i = 0; i < targetsPerWave; i++)
                {
                    GameObject spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
                    Vector3 spawnPos = spawnPoint.transform.position;

                    GameObject chosenPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)];

                    GameObject instance = Instantiate(chosenPrefab, spawnPos, Quaternion.identity);
                    Target targetScript = instance.GetComponent<Target>();

                    Transform[] randomizedPath = GetRandomizedWaypoints(waypointCountPerTarget);
                    targetScript.SetWayPoints(randomizedPath);

                    spawnedTargets[i] = instance;

                    yield return new WaitForSeconds(spawnDelay);
                }

                while (spawnedTargets.Any(targetsPerWave => targetsPerWave != null))
                {
                    yield return null;
                }

                waveCount++;
            }

        }

        private Transform[] GetRandomizedWaypoints(int count)
        {
            return wayPoints.OrderBy(x => Random.value).Take(count).ToArray();
        }
    }
}