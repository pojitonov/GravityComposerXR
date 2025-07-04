using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    public class FixedTimestamp : ITimestamp
    {
        [ShowInInspector, HideInEditorMode]
        public int EndTick => _endTick;

        [ShowInInspector, HideInEditorMode]
        public int RemainingTicks => this.GetRemainingTicks();

        [ShowInInspector, HideInEditorMode]
        public float RemainingTime => this.GetRemainingTime();

        private int _endTick;

        public FixedTimestamp(int endTick = -1)
        {
            _endTick = endTick;
        }

        [Button]
        public void StartFromSeconds(float seconds)
        {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException(nameof(seconds));

            _endTick = Mathf.RoundToInt((Time.fixedTime + seconds) / Time.fixedDeltaTime);
        }

        [Button]
        public void StartFromTicks(int ticks)
        {
            if (ticks < 0)
                throw new ArgumentOutOfRangeException(nameof(ticks));

            this.StartFromSeconds(ticks * Time.fixedDeltaTime);
        }

        [Button]
        public void Stop()
        {
            _endTick = -1;
        }

        public float GetProgress(float duration)
        {
            return 1 - this.GetRemainingTime() / duration;
        }

        private float GetRemainingTime()
        {
            int ticks = this.GetRemainingTicks();
            return ticks != -1 ? ticks * Time.fixedDeltaTime : default;
        }

        private int GetRemainingTicks()
        {
            return _endTick > 0
                ? Math.Max(0, _endTick - Mathf.RoundToInt(Time.fixedTime / Time.fixedDeltaTime))
                : 0;
        }

        public bool IsIdle()
        {
            return _endTick == -1;
        }

        public bool IsPlaying()
        {
            return _endTick > 0 && _endTick > CurrentTick();
        }

        private static int CurrentTick()
        {
            return Mathf.RoundToInt(Time.fixedTime / Time.fixedDeltaTime);
        }

        public bool IsExpired()
        {
            return _endTick > 0 && _endTick <= CurrentTick();
        }
    }
}