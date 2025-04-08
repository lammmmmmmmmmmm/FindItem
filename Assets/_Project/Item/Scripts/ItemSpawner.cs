using System.Collections.Generic;
using UnityEngine;

namespace Item {
    public class ItemSpawner : MonoBehaviour {
        [SerializeField] private int numberOfItemsToSpawn = 15;
        [SerializeField] private float spawnRadius = 20f;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private LayerMask noSpawnLayerMask;

        private int itemSpawned;

        private void Start()
        {
            itemSpawned = 0;
        }

        public void SetNumberOfItemsToSpawn(int numberOfItems) {
            numberOfItemsToSpawn = numberOfItems;
        }
        
        public void SpawnItems() {
            Collider2D[] results = new Collider2D[1];
            int numSpawn = numberOfItemsToSpawn - itemSpawned;

            for (int i = 0; i < numberOfItemsToSpawn; i++) {
                Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
                
                // check if there is no wall at the random position
                var size = Physics2D.OverlapCircleNonAlloc(randomPosition, 0.5f, results, noSpawnLayerMask);
                if (size > 0) {
                    i--;
                    continue;
                }
                
                Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0) + transform.position;
                Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                itemSpawned++;
            }
        }
    }
}