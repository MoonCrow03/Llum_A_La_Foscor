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
        [SerializeField] private GameManager m_GameManager;

        private void Start()
        {
            interactkeyUI = GameObject.Find("FkeyInteract").GetComponent<SpriteRenderer>();
            interactkeyUI.enabled = false;
            
            if (GameManager.Instance != null && GameManager.Instance.m_IsWorldCompleted)
            {
                interactkeyUI.enabled = false;
            }
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