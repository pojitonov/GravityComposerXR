using System;
using Atomic.Elements;
using Sirenix.OdinInspector;

namespace Atomic.Networking.Elements
{
    public class NetworkTimestamp : ITimestamp, IDisposable, INetworkObject.IInit
    {
        [ShowInInspector, HideInEditorMode]
        public int EndTick => _object.IsActive ? this.endTick() : -1;

        [ShowInInspector, HideInEditorMode]
        public int RemainingTicks => this.GetRemainingTicks();

        [ShowInInspector, HideInEditorMode]
        public float RemainingTime => this.GetRemainingTime();

        private readonly INetworkObject _object;
        private readonly int _tickPtr;

        public NetworkTimestamp(INetworkObject obj)
        {
            _object = obj ?? throw new ArgumentNullException(nameof(obj));
            _tickPtr = obj.AllocState<int>();
            
            _object.AddListener(this);
        }

        public void Dispose()
        {
            _object.FreeState<int>(_tickPtr);
            _object.RemoveListener(this);
        }

        void INetworkObject.IInit.OnInit()
        {
            this.endTick() = -1;
        }
        
        [Button]
        public void StartFromSeconds(float seconds)
        {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException(nameof(seconds));

            INetworkFacade facade = _object.Facade;
            if (facade is not {IsActive: true})
                return;

            if (!_object.IsActive)
                return;

            this.endTick() = facade.Tick + (int) Math.Ceiling((double) seconds / facade.DeltaTime);
        }

        [Button]
        public void StartFromTicks(int ticks)
        {
            if (ticks < 0)
                throw new ArgumentOutOfRangeException(nameof(ticks));
            
            INetworkFacade facade = _object.Facade;
            if (facade is not {IsActive: true})
                return;

            if (!_object.IsActive)
                return;

            this.endTick() = facade.Tick + ticks;
        }

        [Button]
        public void Stop()
        {
            if (_object.IsActive) 
                this.endTick() = -1;
        }

        public float GetProgress(float duration)
        {
            return _object.IsActive ? 1 - this.GetRemainingTime() / duration : -1;
        }

        private float GetRemainingTime()
        {
            int ticks = this.GetRemainingTicks();
            return ticks != -1 ? ticks * _object.Facade.DeltaTime : default;
        }

        private int GetRemainingTicks()
        {
            INetworkFacade facade = _object.Facade;
            if (facade is not {IsActive: true})
                return -1;

            if (!_object.IsActive)
                return -1;

            int tick = this.endTick();
            return tick > 0 ? Math.Max(0, tick - facade.Tick) : 0;
        }

        public bool IsIdle()
        {
            return !_object.IsActive || this.endTick() == -1;
        }

        public bool IsPlaying()
        {
            INetworkFacade facade = _object.Facade;
            if (facade is not {IsActive: true})
                return false;

            if (!_object.IsActive)
                return false;

            int tick = this.endTick();
            return tick > 0 && facade.Tick < tick;
        }

        public bool IsExpired()
        {
            INetworkFacade facade = _object.Facade;
            if (facade is not {IsActive: true})
                return true;

            if (!_object.IsActive)
                return true;

            int tick = this.endTick();
            return tick > 0 && tick <= facade.Tick;
        }

        private ref int endTick() => ref _object.GetState<int>(_tickPtr);
    }
}