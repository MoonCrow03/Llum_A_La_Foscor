using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace WorldScripts
{
    public class StepSoundManager : MonoBehaviour
    {
        
        [SerializeField] private EventReference m_StepSoundEvent;
        
        private EventInstance m_StepSoundEmitter;

        private void Awake()
        {
            m_StepSoundEmitter = RuntimeManager.CreateInstance(m_StepSoundEvent);
        }

        private void Update()
        {
            
            m_StepSoundEmitter.getPlaybackState(out var playbackState);
            if (PlayerController.IsMoving)
            {
                float randomPitch = UnityEngine.Random.Range(0.5f, 2f);
                m_StepSoundEmitter.setPitch(randomPitch);
                Debug.Log("Playing sound with pitch: " + randomPitch);
                if (playbackState == PLAYBACK_STATE.PLAYING) return;
                m_StepSoundEmitter.start();
            }
            else
            {
                m_StepSoundEmitter.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }

        private void OnDestroy()
        {
            m_StepSoundEmitter.release();
        }
    }
}