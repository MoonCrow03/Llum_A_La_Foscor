using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace WorldScripts
{
    public class TriggerPossibleSpawnLocations : MonoBehaviour
    {
        public List<SceneSpawnLocation> sceneSpawnLocations = new List<SceneSpawnLocation>();
        private Dictionary<string, List<Transform>> spawnPossibleLocations = new Dictionary<string, List<Transform>>();

        private void OnEnable()
        {
            BasicSceneChanger.OnSceneChange += PlaceTriggerInRandomLocation;
        }
        
        private void OnDisable()
        {
            BasicSceneChanger.OnSceneChange -= PlaceTriggerInRandomLocation;
        }

        private void Awake()
        {
            foreach (var item in sceneSpawnLocations)
            {
                spawnPossibleLocations[item.sceneName] = item.spawnLocations;
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
            if (spawnPossibleLocations.TryGetValue(sceneName, out var possibleLocations))
            {
                if (possibleLocations.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, possibleLocations.Count);
                    Transform randomLocation = possibleLocations[randomIndex];
                    SceneSpawnLocation sceneSpawnLocation = sceneSpawnLocations.Find(x => x.sceneName == sceneName);
                    Instantiate(sceneSpawnLocation.spawnTrigger, randomLocation.position, Quaternion.identity);
                }
            }
        }
    }

    [Serializable]
    public class SceneSpawnLocation
    {
        public string sceneName;
        public GameObject spawnTrigger;
        public List<Transform> spawnLocations;
    }
}