using System;
using System.Collections.Generic;
using UnityEngine;

namespace WorldScripts
{
    public class Trigger : MonoBehaviour
    {
        public List<Transform> spawnLocations;
        public Scenes sceneToLoad;

        [SerializeField] SpriteRenderer interactkeyUI;

        private void Start()
        {
            interactkeyUI = GameObject.Find("FkeyInteract").GetComponent<SpriteRenderer>();
            interactkeyUI.enabled = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactkeyUI.enabled = true;

                if (InputManager.Instance.F.Tap)
                {
                    GameEvents.TriggerSetPlayerPosition();
                    GameManager.Instance.LoadScene(sceneToLoad);

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactkeyUI.enabled = false;
            }
        }
    }
}