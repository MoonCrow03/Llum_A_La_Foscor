using System;
using FMODUnity;
using UnityEngine;

namespace WorldScripts
{
    public class StepSoundManager : MonoBehaviour
    {
        [SerializeField] private StudioEventEmitter m_StepSoundEmitter;
        private void Update()
        {
            if (PlayerController.IsMoving)
            {
                if (m_StepSoundEmitter.IsPlaying()) return;
                m_StepSoundEmitter.Play();
            }
            else
            {
                m_StepSoundEmitter.Stop();
            }
        }
    }
}