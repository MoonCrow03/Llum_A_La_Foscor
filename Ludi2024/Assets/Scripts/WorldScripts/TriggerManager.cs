using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldScripts
{
    public class TriggerManager : MonoBehaviour
    {
        [SerializeField] private List<Trigger> triggers;
        
        private void Start()
        {
            PlaceTriggers();
        }

        private void PlaceTriggers()
        {
            foreach (var trigger in triggers)
            {
                if (GameManager.Instance.IsMiniGameCompleted(trigger.sceneToLoad))
                {
                    trigger.gameObject.SetActive(false);
                    continue;
                }
                Transform spawnLocation = trigger.spawnLocations[Random.Range(0, trigger.spawnLocations.Count)];
                trigger.transform.position = spawnLocation.position;
                trigger.transform.rotation = spawnLocation.rotation;
            }
        }
    }
}