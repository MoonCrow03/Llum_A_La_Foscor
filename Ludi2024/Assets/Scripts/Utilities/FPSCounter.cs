using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities
{
    public class FPSCounter : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;

        private const float PollingTime = 1f;
        private float time;
        private int frameCount;

        private void Update()
        {
            time += Time.deltaTime;
            frameCount++;

            if (!(time >= PollingTime)) return;
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate + " FPS";

            fpsText.color = frameRate switch
            {
                >= 60 => Color.green,
                >= 30 => Color.yellow,
                _ => Color.red
            };

            time -= PollingTime;
            frameCount = 0;
        }
    }
}