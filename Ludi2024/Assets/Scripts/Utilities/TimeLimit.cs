using UnityEngine;
using System;
using System.Collections;

namespace Utilities
{
    public class TimeLimit
    {
        private float timeRemaining;
        private float totalTime;
        private Action onTimerEnd;
        private MonoBehaviour coroutineOwner;
        

        public TimeLimit(MonoBehaviour owner)
        {
            coroutineOwner = owner;
        }
        
        public void StartTimer(float seconds, Action onTimerEndParam)
        {
            totalTime = seconds;
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
        
        
        public float GetTimeRemaining()
        {
            return timeRemaining;
        }
        

        public int GetPoints(float multiplier)
        {
            return Mathf.CeilToInt(timeRemaining * multiplier);
        }

        public int GetNumOfStars()
        {
            float sectorDuration = totalTime / 3;
            
            if (timeRemaining > sectorDuration * 2)
            {
                return 3;
            }
            else if (timeRemaining > sectorDuration)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
    
  
}