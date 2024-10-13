﻿using UnityEngine;
using System;
using System.Collections;

namespace Utilities
{
    public class TimeLimit
    {
        private float timeRemaining;
        private Action onTimerEnd;
        private MonoBehaviour coroutineOwner;

        public TimeLimit(MonoBehaviour owner)
        {
            coroutineOwner = owner;
        }
        
        public void StartTimer(float seconds, Action onTimerEndParam)
        {
            timeRemaining = seconds;
            onTimerEnd = onTimerEndParam;
            coroutineOwner.StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            while (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                yield return null;
            }

            onTimerEnd?.Invoke();
        }
        
        public void StopTimer()
        {
            coroutineOwner.StopAllCoroutines();
        }

        public bool IsTimeUp()
        {
            return timeRemaining <= 0;
        }
        
        public float GetTimeRemaining()
        {
            return timeRemaining;
        }
    }
    
  
}