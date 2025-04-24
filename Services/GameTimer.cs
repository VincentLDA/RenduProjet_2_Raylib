using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S.Services
{

    public class GameTimer
    {
        public float elapsedTime { get; set; } = 0f;
        public float duration { get; private set; }
        public bool isLooping { get; set; }
        public bool isRunning { get; private set; }
        public Action? Callback { private get; set; }

        public GameTimer(float duration, Action? callback = null, bool isLooping = true)
        {
            this.duration = duration;
            this.isLooping = isLooping;
            this.Callback = callback;
            elapsedTime = 0f;
            isRunning = true;
        }

        public void Update(float deltaTime)
        {
            if (!isRunning) return;
            elapsedTime += deltaTime;
            if (elapsedTime >= duration)
            {

                Callback?.Invoke();
                if (isLooping)
                {
                    elapsedTime = 0f;
                }
                else
                {
                    Stop();
                }
            }
        }

        public void Start()
        {
            elapsedTime = 0f;
            isRunning = true;
        }

        public void pause()
        {
            isRunning = false;
        }

        public void Stop()
        {
            Reset();
            isRunning = false;
        }

        public void Reset()
        {
            elapsedTime = 0f;
        }

        public void SetDuration(float newDuration)
        {
            duration = newDuration;
        }

        public bool IsFinished()
        {
            return elapsedTime >= duration;
        }
    }


}
