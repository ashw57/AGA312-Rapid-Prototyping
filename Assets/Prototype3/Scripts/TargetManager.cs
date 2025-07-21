using Prototype3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Prototype3
{
    public enum TargetType
    {
        Average,
        Slow,
        Fast
    }

    public class TargetManager : Singleton<TargetManager>
    {
        public int initialSpawnCount = 6;
        public float initialSpawnDelay = 1f;
        public Transform[] spawnPoints;
        public GameObject[] targetTypes;
        public List<Target> targets;

        public int TargetCount => targets.Count;
        public bool NoTargets => targets.Count == 0;

        public Transform GetSpecificSpawnPoint(int _spawnPoint) => spawnPoints[_spawnPoint];

        void Start()
        {
            print(TargetCount);
            StartCoroutine(SpawnWithDelay(initialSpawnCount, initialSpawnDelay));
        }

        private IEnumerator SpawnWithDelay(int _spawnCount, float _spawnDelay)
        {


            for (int i = 0; i < _spawnCount; i++)
            {
                yield return new WaitForSeconds(_spawnDelay);
                if (_CurrentGameState == GameState.Playing)
                    SpawnTarget();
            }

        }

        private void SpawnTarget()
        {
            int rndEnemy = Random.Range(0, targetTypes.Length);
            int rndSpawn = Random.Range(0, spawnPoints.Length);
            GameObject target = Instantiate(targetTypes[rndEnemy], spawnPoints[rndSpawn].transform.position, spawnPoints[rndSpawn].transform.rotation);

            Target targetComp = target.GetComponent<Target>();
            if (target != null)
            {
                targetComp.Initialize(spawnPoints[rndSpawn], $"Target_{targets.Count + 1}");
                targets.Add(targetComp);
            }
        }
    }
}
