using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delegates
{
    public class Spawner : MonoBehaviour
    {
        public Transform target;
        public GameObject orcPrefab, trollPrefab;
        public float minAmount = 0, maxAmount = 20;
        public float spawnRate = 1f;

        // Use this for initialization
        void SpawnTroll()
        {
            // Spawn Troll Prefab
            // SetTarget on troll to target
        }

        // Update is called once per frame
        void SpawnOrc()
        {

           // Spawn Orc Prefab
           // SetTarget on orc to target
        }

        // Goal is to call these two functions
        // randomly using delegates
    }
}

