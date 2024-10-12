using System;
using UnityEngine;

namespace Utilities
{
    public class BasicSceneChanger : MonoBehaviour
    {
        public string sceneToLoad;
        
        private static BasicSceneChanger instance;
        
        public static BasicSceneChanger Instance => instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void ChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad);
        }
        
        public static void ChangeScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
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