using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace Utilities
{
    public class SoundEffectsManager : MonoBehaviour
    {
        [SerializeField] private List<SerializedEventReference> soundEffects;
        private static SoundEffectsManager _instance;
        public static SoundEffectsManager Instance => _instance;
    
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            soundEffects = new List<SerializedEventReference>();
        }
    
        public void PlaySoundEffect(string soundEffectName)
        {
            foreach (var soundEffect in soundEffects)
            {
                if (soundEffect.name != soundEffectName) continue;
                RuntimeManager.PlayOneShot(soundEffect.eventReference);
                return;
            }
        }
    }
    
    [System.Serializable]
    public class SerializedEventReference
    {
        public string name;
        public FMODUnity.EventReference eventReference;
    }
}
