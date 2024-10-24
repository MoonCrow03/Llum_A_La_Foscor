using System.Collections.Generic;
using UnityEngine;

namespace WorldScripts
{
    public class TriggerManager : MonoBehaviour
    {
        [SerializeField] private List<Trigger> triggers;

        private void PlaceTriggers()
        {
            foreach (var trigger in triggers)
            {
                Transform spawnLocation = trigger.spawnLocations[Random.Range(0, trigger.spawnLocations.Count)];
                Instantiate(trigger, spawnLocation.position, Quaternion.identity);
            }
        }
        
        
    }
}