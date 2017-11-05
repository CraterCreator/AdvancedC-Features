using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Recursion
{
    public class RecursiveSpawner : MonoBehaviour
    {
        public GameObject spawnPrefab;
        public int amount = 10;
        public float positionOffset = 1f;
        public float scaleFactor = 0.9f;

        // Recursively spawns an object by calling itself
        void RecursiveSpawn(int currentAmount, Vector3 position, Vector3 scale)
        {
            amount--;

            if(amount <= 0)
            {
                return;
            }

            // Calculate offsets
            Vector3 adjustedScale = scale * scaleFactor;
            Vector3 adjustedPosition = position + Vector3.up * adjustedScale.magnitude;

            // Instantiate spawn prefab
            GameObject clone = Instantiate(spawnPrefab);
            clone.transform.position = adjustedPosition;
            clone.transform.localScale = adjustedScale;

            // Call itself
            RecursiveSpawn(amount, adjustedPosition, adjustedScale);
        }

        // Use this for initialization
        void Start()
        {
            Vector3 position = transform.position;
            Vector3 scale = spawnPrefab.transform.localScale;
            RecursiveSpawn(amount, position, scale);
            
        }
    }
}
