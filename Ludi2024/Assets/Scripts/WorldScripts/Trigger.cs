using System;
using System.Collections.Generic;
using UnityEngine;

namespace WorldScripts
{
    public class Trigger : MonoBehaviour
    {
        public List<Transform> spawnLocations;
        public Scenes sceneToLoad;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (InputManager.Instance.F.Tap)
                {
                    GameEvents.TriggerSetPlayerPosition();
                    GameManager.Instance.LoadScene(sceneToLoad);
                }
            }
        }

        
    }
}