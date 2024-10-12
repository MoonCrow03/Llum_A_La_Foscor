using System;
using UnityEngine;

namespace Utilities
{
    public class BasicSceneChanger : MonoBehaviour
    {
        public string sceneToLoad;
        
        public void ChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered trigger. Changing scene.");
                ChangeScene();
            }
        }
    }
}