using System;
using TMPro;
using UnityEngine;

namespace AgeOfKing.Utils
{
    public class FPSTOOL : MonoBehaviour
    {
        public int TargetFPS = 15;
        public float updateInterval = 0.5f;
        public bool LockFPS;
        public bool ShowFPS;

        public bool showMedian = false;
        public float medianLearnrate = 0.05f;

        private float accum = 0; // FPS accumulated over the interval
        private int frames = 0; // Frames drawn over the interval
        private float timeleft; // Left time for current interval
        private float currentFPS = 0;

        private float median = 0;
        private float average = 0;

        public float CurrentFPS
        {
            get { return currentFPS; }
        }

        public float FPSMedian
        {
            get { return median; }
        }

        public float FPSAverage
        {
            get { return average; }
        }

        public TextMeshProUGUI uguiText;

        void Start()
        {
            if (LockFPS)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = TargetFPS;
            }

            if (ShowFPS == false)
            {
                return;
            }

            timeleft = updateInterval;
            uguiText.gameObject.SetActive(true);
        }


        void Update()
        {
            if (LockFPS)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = TargetFPS;
            }

            if (ShowFPS == false)
            {
                enabled = false;
                return;
            }

            // Timing inside the editor is not accurate. Only use in actual build.

            //#if !UNITY_EDITOR

            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            // Interval ended - update GUI text and start new interval
            if (timeleft <= 0.0)
            {
                currentFPS = accum / frames;

                average += (Mathf.Abs(currentFPS) - average) * 0.1f;
                median += Mathf.Sign(currentFPS - median) * Mathf.Min(average * medianLearnrate, Mathf.Abs(currentFPS - median));

                // display two fractional digits (f2 format)
                float fps = showMedian ? median : currentFPS;

                if(uguiText != null)
                    uguiText.text = String.Format("{0:F2} FPS ({1:F1} ms)", fps, 1000.0f / fps);

                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;
            }
            //#endif
        }

        public void ResetMedianAndAverage()
        {
            median = 0;
            average = 0;
        }
    }

}
