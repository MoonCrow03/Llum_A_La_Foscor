using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace WorldScripts
{
    public class TriggerPossibleSpawnLocations : MonoBehaviour
    {
        public List<SceneSpawnLocation> sceneSpawnLocations = new List<SceneSpawnLocation>();
        private Dictionary<Scenes, List<Transform>> spawnPossibleLocations = new Dictionary<Scenes, List<Transform>>();

        private void OnEnable()
        {
            BasicSceneChanger.OnSceneChange += StartRandomLocationGen;
        }
        
        private void OnDisable()
        {
            BasicSceneChanger.OnSceneChange -= StartRandomLocationGen;
        }

        private void Awake()
        {
            foreach (var item in sceneSpawnLocations)
            {
                spawnPossibleLocations[item.sceneID] = item.spawnLocations;
            }
        }

        private void Start()
        {
            foreach (var sceneSpawn in sceneSpawnLocations)
            {
                PlaceTriggerInRandomLocation(sceneSpawn.sceneID);
            }
        }

        private void StartRandomLocationGen() {
            foreach (var sceneSpawn in sceneSpawnLocations)
            {
                PlaceTriggerInRandomLocation(sceneSpawn.sceneID);
            }
        }
        private void PlaceTriggerInRandomLocation(Scenes levelCompleted)
        {
            if (spawnPossibleLocations.ContainsKey(levelCompleted))
            {
                var spawnLocations = spawnPossibleLocations[levelCompleted];
                var spawnLocation = sceneSpawnLocations.Find(x => x.sceneID == levelCompleted);
                var randomLocation = spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Count)];
                Instantiate(spawnLocation.spawnTrigger, randomLocation.position, randomLocation.rotation);
            }
        }
    }

    [Serializable]
    public class SceneSpawnLocation
    {
        public Scenes sceneID;
        public GameObject spawnTrigger;
        public List<Transform> spawnLocations;
    }
}