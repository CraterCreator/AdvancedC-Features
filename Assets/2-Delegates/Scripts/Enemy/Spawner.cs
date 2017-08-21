using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delegates
{
    public class Spawner : MonoBehaviour
    {
        public Transform target;
        public GameObject orcPrefab, trollPrefab;
        public int minAmount = 0, maxAmount = 20;
        public float spawnRate = 1f;

        delegate void Prefabs();

        private List<Prefabs> Spawning = new List<Prefabs>();


        void Awake()
        {
            Spawning.Add(SpawnOrc);
            Spawning.Add(SpawnTroll);
        }

        void Start()
        {
            if(Enemy.target != null)
            {
            InvokeRepeating("SpawnLoop", 1, 1);

            }
        }

        // Use this for initialization
        void SpawnTroll()
        {
            // Spawn Troll Prefab
            Instantiate(trollPrefab, transform.position,transform.rotation);
            
        }

        // Update is called once per frame
        void SpawnOrc()
        {
            // Spawn Orc Prefab
            Instantiate(orcPrefab, transform.position, transform.rotation);
        }

        void SpawnLoop()
        {
            // Goal is to call these two functions
            // randomly using delegates
            Spawning[Random.Range(0, 2)]();
        }
    }
}

