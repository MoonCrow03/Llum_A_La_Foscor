using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldScripts
{
    public class TriggerManager : MonoBehaviour
    {
        [SerializeField] private List<Trigger> triggers;

        private void PlaceTriggers()
        {
            foreach (var trigger in triggers)
            {
                if (GameManager.Instance.IsMiniGameCompleted(trigger.sceneToLoad))
                {
                    continue;
                }
                Transform spawnLocation = trigger.spawnLocations[Random.Range(0, trigger.spawnLocations.Count)];
                Instantiate(trigger, spawnLocation.position, Quaternion.identity);
            }
        }

        private void Start()
        {
            PlaceTriggers();
        }
    }
}