using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Map {
    public class MapSpawner : MonoBehaviour {
        private MapData _mapDataPrefab;
        public UnityEvent<MapData> onMapSpawnedEvent;

        public void SetRandomMaps(List<MapData> mapPrefabs) {
            int randomIndex = Random.Range(0, mapPrefabs.Count);
            _mapDataPrefab = mapPrefabs[randomIndex];
            Debug.Log("Map chosen: " + _mapDataPrefab.name);
        }

        public void SpawnMap() {
            if (!_mapDataPrefab) {
                Debug.LogWarning("No maps available to spawn.");
                return;
            }

            var mapSpawnArea = Instantiate(_mapDataPrefab, transform.position, Quaternion.identity);
            AstarPath.active.Scan();
            onMapSpawnedEvent.Invoke(mapSpawnArea);
        }
    }
}