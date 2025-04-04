using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Map {
    public class MapSpawner : MonoBehaviour {
        private MapSpawnArea _mapSpawnAreaPrefab;
        public UnityEvent<MapSpawnArea> onMapSpawnedEvent;

        public void SetRandomMaps(List<MapSpawnArea> mapPrefabs) {
            int randomIndex = Random.Range(0, mapPrefabs.Count);
            _mapSpawnAreaPrefab = mapPrefabs[randomIndex];
            Debug.Log("Map chosen: " + _mapSpawnAreaPrefab.name);
        }

        public void SpawnMap() {
            if (!_mapSpawnAreaPrefab) {
                Debug.LogWarning("No maps available to spawn.");
                return;
            }

            var mapSpawnArea = Instantiate(_mapSpawnAreaPrefab, transform.position, Quaternion.identity);
            AstarPath.active.Scan();
            onMapSpawnedEvent.Invoke(mapSpawnArea);
        }
    }
}