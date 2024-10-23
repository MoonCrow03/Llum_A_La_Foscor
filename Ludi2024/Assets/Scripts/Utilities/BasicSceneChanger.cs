using System;
using UnityEngine;

namespace Utilities
{
    public class BasicSceneChanger : MonoBehaviour
    {
        public string sceneToLoad;
        public ELevelsCompleted level;
        public static event Action OnSceneChange;
        
        public void ChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad);
        }
        
        public static void ChangeScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            OnSceneChange?.Invoke();
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