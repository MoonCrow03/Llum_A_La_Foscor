using System;
using UnityEngine;

namespace Utilities
{
    public class BasicSceneChanger : MonoBehaviour
    {
        public string sceneToLoad;
        public static event Action<string> OnSceneChange;
        
        public void ChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad);
        }
        
        public static void ChangeScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            OnSceneChange?.Invoke(sceneName);
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