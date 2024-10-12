using System;
using System.Collections.Generic;
using UnityEngine;

namespace WorldScripts
{
    public class TriggerPossibleSpawnLocations : MonoBehaviour
    {
        public List<SceneSpawnLocation> sceneSpawnLocations = new List<SceneSpawnLocation>();
        public Dictionary<string, List<Transform>> possibleSpawnLocations = new Dictionary<string, List<Transform>>();

        private void Awake()
        {
            foreach (var item in sceneSpawnLocations)
            {
                possibleSpawnLocations[item.sceneName] = item.spawnLocations;
            }
        }

        private void Start()
        {
            foreach (var sceneSpawn in sceneSpawnLocations)
            {
                PlaceTriggerInRandomLocation(sceneSpawn.sceneName);
            }
        }

        private void PlaceTriggerInRandomLocation(string sceneName)
        {
            if (possibleSpawnLocations.TryGetValue(sceneName, out var possibleLocations))
            {
                if (possibleLocations.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, possibleLocations.Count);
                    Transform randomLocation = possibleLocations[randomIndex];
                    transform.position = randomLocation.position;
                    transform.rotation = randomLocation.rotation;
                }
            }
        }
    }

    [Serializable]
    public class SceneSpawnLocation
    {
        public string sceneName;
        public List<Transform> spawnLocations;
    }
}